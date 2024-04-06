using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IReservationRoomService
    {
        Task<object> GetAllReservationRoom();
        Task<ReservationRoom> GetReservationRoomByRoomID(string ID);
        Task<object> GetAllReservationRoomByReservationID(string ID);
        Task<object> CreateReservationRoom(ReservationRoom model);
        Task<object> DeleteAllReservationRoom();
    }
}
