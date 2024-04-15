using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;
using System.Security.Cryptography;

namespace BackEnd.Services.Interfaces
{
    public interface IAuthService
    {
        Task<object> Login(LoginVM model);
        Task<string> CreateAccessToken(User user);
        string CreateRefreshToken();
        Task<Token> CreateToken(User user);
        Task<object> RefeshToken(string accessToken, string refeshToken);
    }
}
