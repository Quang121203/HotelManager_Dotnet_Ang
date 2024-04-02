using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<object> GetRoomType(string id);
        Task<object> GetAllRoomType();
        Task<object> CreateRoomType(RoomType roomType);
        Task<object> UpdateRoomType(RoomType roomType);
        Task<object> DeleteRoomType(string id);

    }
}
