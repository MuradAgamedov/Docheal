using Doccure.NurseService.Dtos.NurseDtos;
using Doccure.NurseService.Services.NurseServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.NurseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurseService _service;

        public NursesController(INurseService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _service.GetByIdAsync(id);
            return value == null ? NotFound() : Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNurseDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateNurseDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
