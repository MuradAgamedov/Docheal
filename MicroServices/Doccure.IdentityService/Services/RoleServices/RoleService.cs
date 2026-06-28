using Doccure.IdentityService.Dtos;
using Doccure.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;

namespace Doccure.IdentityService.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        public readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(CreateRoleDto dto)
        {
            if (await _roleManager.RoleExistsAsync(dto.RoleName)) return false;
            var role = new IdentityRole
            {
                Name = dto.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }
    }
}
