using Doccure.Web.UI.Dtos.NurseDtos;

namespace Doccure.Web.UI.Services.NurseServices
{
    public interface INurseService
    {
        Task<List<ResultNurseDto>> GetAllAsync();
        Task<GetByIdNurseDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateNurseDto dto);
        Task UpdateAsync(UpdateNurseDto dto);
        Task DeleteAsync(int id);
    }
}
