using Doccure.Web.UI.Dtos.MedicineDtos;

namespace Doccure.Web.UI.Services.MedicineServices
{
    public interface IMedicineService
    {
        Task<List<ResultMedicineDto>> GetAllAsync();
        Task<ResultMedicineDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateMedicineDto dto);
        Task UpdateAsync(UpdateMedicineDto dto);
        Task DeleteAsync(int id);
        Task<string?> UploadImageAsync(IFormFile file);
    }
}
