using Doccure.IdentityService.Dtos;
using Doccure.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Doccure.IdentityService.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) { 
                return null;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
            {
                return null;
            }

            return await GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var user = new AppUser { 
                UserName=dto.Username,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                City = dto.City,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded;
        }




        public async Task<string> GenerateJwtToken(AppUser user)
        {
            var secretKey = _configuration.GetValue<string>("Jwt:Key");
            var issuer = _configuration.GetValue<string>("Jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var expireMinutes = _configuration.GetValue<int>("Jwt:ExpireMinutes");
            var roles = await _userManager.GetRolesAsync(user);
            // 1. Define user-specific claims (identity details)
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", user.Id),
            new Claim("Name", user.Name),
            new Claim("Surname", user.Surname),
            new Claim("Email", user.Email),
        };
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // 2. Create the cryptographic security key from your secret config
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Configure the token structure and expiration rules
            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes), // Token lifespan
                signingCredentials: credentials);

            // 4. Serialize the token object into a compact string format
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
