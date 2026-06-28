using Doccure.Web.UI.Dtos.BranchDtos;

namespace Doccure.Web.UI.Services.BranchServices
{
    public interface IBranchService
    {
        Task<List<ResultBranchDto>> GetAllAsync();
        Task<GetBranchByIdDto> GetByIdAsync(string id);
        Task CreateAsync(CreateBranchDto dto);
        Task UpdateAsync(UpdateBranchDto dto);
        Task DeleteAsync(string id);
    }
}
