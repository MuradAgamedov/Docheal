using Doccure.Web.UI.Dtos.DoctorDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doccure.Web.UI.Services.DoctorServices
{
    public interface IDoctorService
    {
        Task<List<ResultDoctorDto>> GetAllAsync();
        Task<GetDoctorByIdDto> GetByIdAsync(string id);
        Task CreateAsync(CreateDoctorDto dto);
        Task UpdateAsync(UpdateDoctorDto dto);
        Task DeleteAsync(string id);
    }
}
