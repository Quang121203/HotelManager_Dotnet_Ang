using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BackEnd.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> CreateAccessToken(User user)
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

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<Token> CreateToken(User user)
        {
            string accessToken = await CreateAccessToken(user);
            string refeshToken = CreateRefreshToken();

            Token token = new Token
            {
                RefeshToken = refeshToken,
                ExpiresRefeshToken = DateTime.Now.AddMinutes(20),
                AccessToken = accessToken,
                UserId = user.Id
            };

            await unitOfWork.TokenRepository.InsertAsync(token);
            await unitOfWork.SaveChangesAsync();


            return token;
        }
        public async Task<object> RefeshToken(string accessToken, string refeshToken)
        {
            var token = await unitOfWork.TokenRepository.GetSingleAsync(refeshToken);

            if (token==null)
            {
                return (new
                {
                    EM = "Token not found",
                    EC = 1,
                    DT = "",
                });
            }

            if (token.ExpiresRefeshToken < DateTime.Now)
            {
                return (new
                {
                    EM = "Token expired",
                    EC = 1,
                    DT = "",
                });
            }

            if (token.AccessToken != accessToken)
            {
                return (new
                {
                    EM = "Invalid Refresh Token",
                    EC = 1,
                    DT = "",
                });
            }

            var user = await userManager.FindByIdAsync(token.UserId);


            await unitOfWork.TokenRepository.DeleteAsync(refeshToken);
            await unitOfWork.SaveChangesAsync();

            token = await CreateToken(user);

            return (new
            {
                EM = "",
                EC = 0,
                DT = token,
            });
        }

        public async Task<object> Login(LoginVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var password = await userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && password)
            {
                Token token = await CreateToken(user);

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
