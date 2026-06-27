using System.Threading.Tasks;
using Doccure.PrescriptionService.Dtos.PrescriptionDtos;
using Doccure.PrescriptionService.Services.PrescriptionServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.PrescriptionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(CreatePrescriptionDto dto)
        {
            await _prescriptionService.CreateAsync(dto);
            return Ok("Prescription created successfully");
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var value = await _prescriptionService.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Prescription not found");
            }
            return Ok(value);
        }

        [HttpGet("GetByAppointmentId")]
        public async Task<IActionResult> GetPrescriptionByAppointmentId(int appointmentId)
        {
            var value = await _prescriptionService.GetByAppintmentIdAsync(appointmentId);
            if (value == null)
            {
                return NotFound("Prescription not found for this appointment");
            }
            return Ok(value);
        }

        [HttpGet("GetByPatientId")]
        public async Task<IActionResult> GetPrescriptionsByPatientId(string patientId)
        {
            var values = await _prescriptionService.GetByPatientIdAsync(patientId);
            return Ok(values);
        }
    }
}
