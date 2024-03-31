using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomType> GetRoomType(string id);
        Task<List<RoomType>> GetAllRoomType();
        Task<bool> CreateRoomType(RoomType roomType);
        Task<bool> UpdateRoomType(RoomType roomType);
        Task<bool> DeleteRoomType(string id);

    }
}
