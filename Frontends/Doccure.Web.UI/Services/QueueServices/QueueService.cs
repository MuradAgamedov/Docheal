using Doccure.Web.UI.Dtos.QueueDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.QueueServices
{
    public class QueueService : IQueueService
    {
        private readonly HttpClient _client;

        public QueueService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ResultPatientQueueDro>> GetAllAsync()
        {
            try
            {
                var responseMessage = await _client.GetAsync("https://localhost:7263/api/Queues");
                if (!responseMessage.IsSuccessStatusCode)
                    return new List<ResultPatientQueueDro>();

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultPatientQueueDro>>(jsonData) ?? new List<ResultPatientQueueDro>();
            }
            catch (Exception)
            {
                // Fallback to empty list or static placeholder logic if microservice is offline
                return new List<ResultPatientQueueDro>();
            }
        }

        public async Task<ResultPatientQueueDro?> GetCurrentPatientAsync()
        {
            try
            {
                var responseMessage = await _client.PostAsync("https://localhost:7263/api/Queues/current?id=0", null);
                if (!responseMessage.IsSuccessStatusCode)
                    return null;

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultPatientQueueDro>(jsonData);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
