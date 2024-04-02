using BackEnd.Models.Domains;


namespace BackEnd.Services.Interfaces
{
    public interface IRoomService
    {
        Task<object> GetAllRoom();
        Task<object> GetRoomById(string roomId);
        Task<object> GetRoomByRoomTypeName(string roomTypeName);
        Task<object> GetRoomByRoomTypeId(string roomTypeId);
        Task<object> GetRoomByIsAvailable(bool status); //lấy phòng còn trống
        Task<bool> CheckRoomByRoomNumber(string roomNumber);
        Task<object> CreateRoom(Room model);
        Task<object> UpdateRoom(Room model);
        Task<object> DeleteAllRoom();
        Task<object> DeleteRoom(string roomId);
        Task<List<Room>> GetRoomIsReser(DateTime StartTime, DateTime EndTime, RoomType RoomType); // lấy phòng đã đặt theo loại phòng
        Task<List<Room>> GetRoomNotReser(DateTime StartTime, DateTime EndTime, RoomType RoomType); // lấy phòng chưa đặt theo loại phòng
        Task<bool> CheckRoom(DateTime StartTime, DateTime EndTime, RoomType RoomType, int NumberRoomWantReser); //kiểm tra phòng còn số lượng phòng trong khoảng time trên theo loại phòng. True là còn phòng 
    }
}
