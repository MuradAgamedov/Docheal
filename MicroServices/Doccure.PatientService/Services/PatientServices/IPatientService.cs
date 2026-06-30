using Doccure.PatientService.Dtos.PatientDtos;

namespace Doccure.PatientService.Services.PatientServices
{
    public interface IPatientService
    {
        Task<List<ResultPatientDto>> GetAllPatientsAsync();
        Task<ResultPatientDto> GetPatientByIdAsync(int id);
        Task CreatePatientAsync(CreatePatientDto dto);
        Task UpdatePatientAsync(UpdatePatientDto dto);
        Task DeletePatientAsync(int id);
    }
}