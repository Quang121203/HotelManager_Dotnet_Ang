using BackEnd.Models.DTOS;

namespace BackEnd.Services.Interfaces
{
    public interface IUserService
    {
        Task<object> CreateUser(UserVM model);
        Task<object> UpdateUser(UserVM model);
        Task<object> DeleteUser(string id);
        Task<object> GetAllUser();
        Task<object> GetUser(string id);
       
    }
}
