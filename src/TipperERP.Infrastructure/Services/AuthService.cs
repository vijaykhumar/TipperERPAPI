using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TipperERP.Application.Auth;
using TipperERP.Application.Users;
using TipperERP.Infrastructure.Security;

namespace TipperERP.Infrastructure.Services;

// Ensure you have the required NuGet package installed for Microsoft.IdentityModel.Tokens.
// Run the following command in the NuGet Package Manager Console:
// Install-Package Microsoft.IdentityModel.Tokens

using Microsoft.IdentityModel.Tokens; // This namespace is part of the Microsoft.IdentityModel.Tokens package.
using TipperERP.Application.Customer;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _config;    

	public AuthService(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;		

	}

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var userEntity = await _userService.GetByUsernameAsync(dto.Username);
        if (userEntity == null || !PasswordHasher.VerifyPassword(dto.Password, userEntity.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new Exception("Jwt:Key not configured"));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userEntity.UserId.ToString()),
            new Claim("userId", userEntity.UserId.ToString()),
            new Claim("roleId", userEntity.RoleId.ToString()),
            new Claim(ClaimTypes.Name, userEntity.Username),
            new Claim(ClaimTypes.Role, userEntity.Role.RoleName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new LoginResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresAt = tokenDescriptor.Expires ?? DateTime.UtcNow.AddHours(8),
            UserId = userEntity.UserId,
            FullName = userEntity.FullName,
            RoleName = userEntity.Role.RoleName
        };
    }
}
