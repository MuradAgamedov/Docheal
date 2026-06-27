using Doccure.BranchService.Dtos.BranchDtos;
using Doccure.BranchService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.BranchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<IActionResult> BranchList() { 
            var values = await _branchService.GetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateBranchDto createBranchDto) { 
            await _branchService.CreateAsync(createBranchDto);
            return Ok("Seccessfull added");
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            await _branchService.DeleteAsync(id);
            return Ok("Seccessfull deletted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBranch(UpdateBranchDto updateBranchDto)
        {
            await _branchService.UpdateAsync(updateBranchDto);
            return Ok("Seccessfull updated");
        }


        [HttpGet("GetBranch")]
        public async Task<IActionResult> GetBranch(string id)
        {
            var value = await _branchService.GetByIdAsync(id);
            return Ok(value);
        }

    }
}
