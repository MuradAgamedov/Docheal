using Doccure.AppointmentService.Dtos.AppointmentDetailDtos;
using Doccure.AppointmentService.Dtos.AppointmentDtos;
using Doccure.AppointmentService.Services.AppointmentDetailServices;
using Doccure.AppointmentService.Services.AppointmentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailsController : ControllerBase
    {
        private readonly IAppointmentDetailService _service;

        public AppointmentDetailsController(IAppointmentDetailService service)
        {
            _service = service;
        }


        [HttpGet("GetAppointmentDetail")]
        public async Task<IActionResult> GetAppointmentDetail(int id)
        {
            var value = await _service.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Appointment detail not found");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentDetail(CreateAppointmentDetailDto createAppointmentDetailDto)
        {
            await _service.CreateAsync(createAppointmentDetailDto);
            return Ok("Appointment detail created successfully");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentDetail(UpdateAppointmentDetailDto updateAppointmentDetailDto)
        {
            await _service.UpdateAsync(updateAppointmentDetailDto);
            return Ok("Appointment detail updated successfully");
        }
    }
}
