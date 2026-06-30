using Doccure.DoctorService.Dtos.DoctorDtos;
using Doccure.DoctorService.Services.DoctorServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.DoctorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService branchService)
        {
            _doctorService = branchService;
        }

        [HttpGet]
        public async Task<IActionResult> DoctorList()
        {
            var values = await _doctorService.GetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateDoctorDto createDoctorDto)
        {
            await _doctorService.CreateAsync(createDoctorDto);
            return Ok("Seccessfull added");
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            await _doctorService.DeleteAsync(id);
            return Ok("Seccessfull deletted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBranch(UpdateDoctorDto updateDoctorDto)
        {
            await _doctorService.UpdateAsync(updateDoctorDto);
            return Ok("Seccessfull updated");
        }


        [HttpGet("GetDoctor")]
        public async Task<IActionResult> GetBranch(string id)
        {
            var value = await _doctorService.GetByIdAsync(id);
            return Ok(value);
        }



        [HttpGet("{id}/summary")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctorNameAndSurnameById(string id)
        {
            var value = await _doctorService.GetDoctorByIdAsync(id);
            return Ok(value);
        }
    }
}
