using Doccure.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.PhoneNumber,
                user.Gender,
                user.BirthDate,
                user.BloodGroup,
                user.ImageUrl,
                user.City,
                user.Address
            });
        }

        /// <summary>
        /// PUT api/Users/{id}  — şəxsi məlumatları yenilə
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.Name        = request.Name        ?? user.Name;
            user.Surname     = request.Surname      ?? user.Surname;
            user.PhoneNumber = request.PhoneNumber  ?? user.PhoneNumber;
            user.Gender      = request.Gender       ?? user.Gender;
            user.BirthDate   = request.BirthDate    ?? user.BirthDate;
            user.City        = request.City         ?? user.City;
            user.ImageUrl    = request.ImageUrl     ?? user.ImageUrl;
            user.BloodGroup  = request.BloodGroup   ?? user.BloodGroup;

            // E-poçtu dəyişdirmək üçün ayrı axın tələb olunur; yalnız fərqlidirsə yenilə
            if (!string.IsNullOrWhiteSpace(request.Email) &&
                !string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                user.Email      = request.Email;
                user.UserName   = request.Email;        // Identity-də UserName = Email (default)
                user.NormalizedEmail    = request.Email.ToUpperInvariant();
                user.NormalizedUserName = request.Email.ToUpperInvariant();
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }

    public class UpdateUserRequest
    {
        public string? Name        { get; set; }
        public string? Surname     { get; set; }
        public string? Email       { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender      { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? City        { get; set; }
        public string? ImageUrl    { get; set; }
        public string? BloodGroup  { get; set; }
    }
}