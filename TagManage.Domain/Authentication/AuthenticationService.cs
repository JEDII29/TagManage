using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using TagManage.Data;
using TagManage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TagManage.Domain.Authentication
{
    public class AuthenticationService(AppDbContext appDbContext, IConfiguration configuration) : IAuthenticationService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly AppDbContext _appDbContext = appDbContext;

        public bool IsValidUser(string username, string password)
        {
            UserEntity user = _appDbContext.Users.FirstOrDefault(x => x.Username == username);
            if (user != null && !string.IsNullOrEmpty(password))
            {
                string hashedPassword = PasswordHasher.HashPassword(password);
                if (user.PasswordHash == hashedPassword)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var id = await appDbContext.Users.Where(x=> x.Username == username).Select(x=>x.Id)
                .FirstOrDefaultAsync();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var handler = new JsonWebTokenHandler();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);
            var claims = new List<Claim>
            {
					new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            var token = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = expiration
            });

            return token;
        }
    }
}
