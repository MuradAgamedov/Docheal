using Doccure.PrescriptionService.Dtos.PrescriptionDtos;

namespace Doccure.PrescriptionService.Services.PrescriptionServices
{
    public class PrescriptionService : IPrescriptionService
    {
        Task IPrescriptionService.CreateAsync(CreatePrescriptionDto dto)
        {
            throw new NotImplementedException();
        }

        Task IPrescriptionService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<ResultPrescriptionDto>> IPrescriptionService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<ResultPrescriptionDto> IPrescriptionService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
