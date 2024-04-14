using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BackEnd.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddSeconds(20),
                claims: claims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object> Login(LoginVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var password = await userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && password)
            {
                string token = await CreateToken(user);

                return (new
                {
                    EM = "login successfully",
                    EC = 0,
                    DT = token
                });
            }

            return (new
            {
                EM = "email or password not right",
                EC = 1,
                DT = ""
            });
        }
    }
}
