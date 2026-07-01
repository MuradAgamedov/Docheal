using Doccure.Web.UI.Dtos.AppointmentDtos;

namespace Doccure.Web.UI.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        Task<List<ResultAppointmentDto>> GetAllAsync();
        Task<GetByIdAppointmentDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateAppointmentDto dto);
        Task UpdateAsync(UpdateAppointmentDto dto);
        Task DeleteAsync(int id);
    }
}
