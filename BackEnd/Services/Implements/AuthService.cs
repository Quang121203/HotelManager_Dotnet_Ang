using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
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
                expires: DateTime.Now.AddMinutes(100000),
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

            if (token == null)
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

                var role = await userManager.GetRolesAsync(user);

                var userVM = new UserVM
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    ID = user.Id,
                    Role = role[0]
                };

                return (new
                {
                    EM = "login successfully",
                    EC = 0,
                    DT = new { token = token, user = userVM }
                });
            }

            return (new
            {
                EM = "email or password not right",
                EC = 1,
                DT = ""
            });
        }

        public object GetInfomation()
        {
            UserVM user = new UserVM();
            if (this.httpContextAccessor.HttpContext != null)
            {
                user.ID = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                user.UserName = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                user.Role = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                user.Email = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }

            return (new
            {
                EM = "",
                EC = 0,
                DT = user
            });

        }

        public async Task<object> ChangePassword(ChangePasswordVM model)
        {
            string userId = "";
            if (this.httpContextAccessor.HttpContext != null)
            {
                userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return (new
                {
                    EM = "User not found",
                    EC = 1,
                    DT = ""
                });
            }

            // Note: Password must be '1234@Abc', one Alphabet,.... (usermanager's problem)
            var result = await this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return (new
                {
                    EM = "Password changed successfully",
                    EC = 0,
                    DT = result
                });
            }

            return (new
            {
                EM = "Fail to change password",
                EC = 1,
                DT = result
            });

        }

        public async Task<object> ChangeInfo(UserVM userVM)
        {
            string userId = "";
            if (this.httpContextAccessor.HttpContext != null)
            {
                userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return (new
                {
                    EM = "User not found",
                    EC = 1,
                    DT = ""
                });
            }

            user.Email = userVM.Email;
            user.UserName = userVM.UserName;

            // Lưu thay đổi vào cơ sở dữ liệu
            var result = await this.userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                Token token = await CreateToken(user);  

                return (new
                {
                    EM = "Info changed successfully",
                    EC = 0,
                    DT = token
                });
            }

            return (new
            {
                EM = "Fail to change info",
                EC = 1,
                DT = result
            });
        }
    }
}
