using Doccure.PatientService.Dtos.PatientDtos;
using Doccure.PatientService.Services.PatientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var values = await _patientService.GetAllPatientsAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var value = await _patientService.GetPatientByIdAsync(id);

            if (value == null)
                return NotFound("Patient not found");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromForm] CreatePatientDto createPatientDto)
        {
            await _patientService.CreatePatientAsync(createPatientDto);
            return Ok("Patient created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromForm] UpdatePatientDto updatePatientDto)
        {
            await _patientService.UpdatePatientAsync(updatePatientDto);
            return Ok("Patient updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return Ok("Patient deleted successfully");
        }
    }
}