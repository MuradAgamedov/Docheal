using Doccure.IdentityService.Dtos;
using Doccure.IdentityService.Services.RoleServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto roleDto)
        {
            var result = await _roleService.CreateRoleAsync(roleDto);
            if (!result)
            {
                return BadRequest("Create failed.");
            }
            return Ok("Create successfully");
        }
    }
}
