using System.Collections.Generic;
using System.Threading.Tasks;
using Doccure.PrescriptionService.Dtos.PrescriptionDtos;

namespace Doccure.PrescriptionService.Services.PrescriptionServices
{
    public interface IPrescriptionService
    {
        Task CreateAsync(CreatePrescriptionDto dto);
        Task<ResultPrescriptionDto> GetByIdAsync(int id);
        Task<ResultPrescriptionDto> GetByAppintmentIdAsync(int appointmentId);
        Task<List<ResultPrescriptionDto>> GetByPatientIdAsync(string patientId);
    }
}
