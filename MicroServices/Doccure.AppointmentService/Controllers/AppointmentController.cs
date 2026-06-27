using Doccure.AppointmentService.Dtos.AppointmentDtos;
using Doccure.AppointmentService.Services.AppointmentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentList()
        {
            var values = await _service.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("GetAppointment")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var value = await _service.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Appointment not found");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto createAppointmentDto)
        {
            await _service.CreateAsync(createAppointmentDto);
            return Ok("Appointment created successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Appointment deleted successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto)
        {
            await _service.UpdateAsync(updateAppointmentDto);
            return Ok("Appointment updated successfully");
        }
    }
}
