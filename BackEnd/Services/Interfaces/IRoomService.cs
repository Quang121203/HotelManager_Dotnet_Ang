using BackEnd.Models.Domains;


namespace BackEnd.Services.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoom();
        Task<Room> GetRoomById(string roomId);
        Task<List<Room>> GetRoomByRoomTypeName(string roomTypeName);
        Task<List<Room>> GetRoomByRoomTypeId(string roomTypeId);
        Task<List<Room>> GetRoomByIsAvailable(bool status); //lấy phòng còn trống
        Task<bool> CheckRoomByRoomNumber(string roomNumber);
        Task<bool> CreateRoom(Room model);
        Task<bool> UpdateRoom(Room model);
        Task<bool> DeleteAllRoom();
        Task<bool> DeleteRoom(string roomId);
        Task<List<Room>> GetRoomIsReser(DateTime StartTime, DateTime EndTime, RoomType RoomType); // lấy phòng đã đặt theo loại phòng
        Task<List<Room>> GetRoomNotReser(DateTime StartTime, DateTime EndTime, RoomType RoomType); // lấy phòng chưa đặt theo loại phòng
        Task<bool> CheckRoom(DateTime StartTime, DateTime EndTime, RoomType RoomType, int NumberRoomWantReser); //kiểm tra phòng còn số lượng phòng trong khoảng time trên theo loại phòng. True là còn phòng 
    }
}
