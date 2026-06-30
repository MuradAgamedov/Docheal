using Doccure.Web.UI.Dtos.PatientDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _client;

        public PatientService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateAsync(CreatePatientDto dto)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Name ?? ""), "Name");
            content.Add(new StringContent(dto.Surname ?? ""), "Surname");
            content.Add(new StringContent(dto.Username ?? ""), "Username");
            content.Add(new StringContent(dto.Email ?? ""), "Email");
            content.Add(new StringContent(dto.Password ?? ""), "Password");
            content.Add(new StringContent(dto.PhoneNumber ?? ""), "PhoneNumber");
            content.Add(new StringContent(dto.BirthDate?.ToString("O") ?? ""), "BirthDate");
            content.Add(new StringContent(dto.Gender ?? ""), "Gender");
            content.Add(new StringContent(dto.BloodGroup ?? ""), "BloodGroup");
            content.Add(new StringContent(dto.City ?? ""), "City");
            content.Add(new StringContent(dto.TcKimlikNo ?? ""), "TcKimlikNo");
            content.Add(new StringContent(dto.InsuranceType ?? ""), "InsuranceType");
            content.Add(new StringContent(dto.Status.ToString()), "Status");

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var fileContent = new StreamContent(dto.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(dto.ImageFile.ContentType);

                content.Add(fileContent, "ImageFile", dto.ImageFile.FileName);
            }

            var responseMessage = await _client.PostAsync("https://localhost:5000/api/patients", content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorBody = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"Xəstə yaradılarkən xəta: {(int)responseMessage.StatusCode} — {errorBody}");
            }
        }

        public async Task UpdateAsync(UpdatePatientDto dto)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.PatientId.ToString()), "PatientId");
            content.Add(new StringContent(dto.AppUserId ?? ""), "AppUserId");
            content.Add(new StringContent(dto.Name ?? ""), "Name");
            content.Add(new StringContent(dto.Surname ?? ""), "Surname");
            content.Add(new StringContent(dto.Email ?? ""), "Email");
            content.Add(new StringContent(dto.PhoneNumber ?? ""), "PhoneNumber");
            content.Add(new StringContent(dto.Gender ?? ""), "Gender");
            content.Add(new StringContent(dto.BloodGroup ?? ""), "BloodGroup");
            content.Add(new StringContent(dto.BirthDate?.ToString("O") ?? ""), "BirthDate");
            content.Add(new StringContent(dto.City ?? ""), "City");
            content.Add(new StringContent(dto.TcKimlikNo ?? ""), "TcKimlikNo");
            content.Add(new StringContent(dto.InsuranceType ?? ""), "InsuranceType");
            content.Add(new StringContent(dto.Status.ToString()), "Status");
            content.Add(new StringContent(dto.ImageUrl ?? ""), "ImageUrl");

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var fileContent = new StreamContent(dto.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(dto.ImageFile.ContentType);

                content.Add(fileContent, "ImageFile", dto.ImageFile.FileName);
            }

            var responseMessage = await _client.PutAsync("https://localhost:5000/api/patients", content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorBody = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"Xəstə yenilənərkən xəta: {(int)responseMessage.StatusCode} — {errorBody}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var responseMessage = await _client.DeleteAsync($"https://localhost:5000/api/patients?id={id}");
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<List<ResultPatientDto>> GetAllAsync()
        {
            var responseMessage = await _client.GetAsync("https://localhost:5000/api/patients");
            if (!responseMessage.IsSuccessStatusCode) return new List<ResultPatientDto>();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultPatientDto>>(jsonData) ?? new List<ResultPatientDto>();
        }

        public async Task<ResultPatientDto> GetByIdAsync(int id)
        {
            var responseMessage = await _client.GetAsync($"https://localhost:5000/api/patients/{id}");
            if (!responseMessage.IsSuccessStatusCode) return null;

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResultPatientDto>(jsonData);
        }
    }
}