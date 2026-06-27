using System.Collections.Generic;
using System.Threading.Tasks;
using Doccure.PrescriptionService.Dtos.PrescriptionDtos;

namespace Doccure.PrescriptionService.Services.PrescriptionServices
{
    public interface IPrescriptionService
    {
        Task<List<ResultPrescriptionDto>> GetAllAsync();
        Task<ResultPrescriptionDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePrescriptionDto dto);
        Task DeleteAsync(int id);
    }
}
