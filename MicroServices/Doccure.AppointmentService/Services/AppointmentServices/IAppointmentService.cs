using Doccure.AppointmentService.Dtos.AppointmentDtos;

namespace Doccure.AppointmentService.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        Task<List<ResultAppointmentDto>> GetAllAsync();
        Task<GetAppointmentById> GetByIdAsync(int id);
        Task CreateAsync(CreateAppointmentDto dto);
        Task UpdateAsync(UpdateAppointmentDto dto);
        Task DeleteAsync(int id);
    }
}
