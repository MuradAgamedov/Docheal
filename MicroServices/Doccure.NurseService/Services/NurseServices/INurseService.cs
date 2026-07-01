using Doccure.NurseService.Dtos.NurseDtos;

namespace Doccure.NurseService.Services.NurseServices
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
