using Doccure.Web.UI.Dtos.QueueDtos;

namespace Doccure.Web.UI.Services.QueueServices
{
    public interface IQueueService
    {
        Task<List<ResultPatientQueueDro>> GetAllAsync();
        Task<ResultPatientQueueDro?> GetCurrentPatientAsync();
    }
}
