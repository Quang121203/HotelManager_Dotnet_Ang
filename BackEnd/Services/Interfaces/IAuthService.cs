using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;

namespace BackEnd.Services.Interfaces
{
    public interface IAuthService
    {
        Task<object> Login(LoginVM model);
        Task<string> CreateToken(User user);
    }
}
