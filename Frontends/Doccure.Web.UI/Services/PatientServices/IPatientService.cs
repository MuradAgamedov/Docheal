using Doccure.Web.UI.Dtos.PatientDtos;

namespace Doccure.Web.UI.Services.PatientServices
{
    public interface IPatientService
    {
        Task<List<ResultPatientDto>> GetAllAsync();
        Task<ResultPatientDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePatientDto dto);
        Task UpdateAsync(UpdatePatientDto dto);
        Task DeleteAsync(int id);
    }
}
