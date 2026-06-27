using Doccure.AppointmentService.Dtos.AppointmentDetailDtos;

namespace Doccure.AppointmentService.Services.AppointmentDetailServices
{
    public interface IAppointmentDetailService
    {
        Task<GetByIdAppointmentDetail> GetByIdAsync(int id);
        Task CreateAsync(CreateAppointmentDetailDto dto);
        Task UpdateAsync(UpdateAppointmentDetailDto dto);
    }
}
