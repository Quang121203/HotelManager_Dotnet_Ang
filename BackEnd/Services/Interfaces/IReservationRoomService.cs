using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IReservationRoomService
    {
        Task<List<ReservationRoom>> GetAllReservationRoom();
        Task<ReservationRoom> GetReservationRoomByRoomID(string ID);
        Task<List<ReservationRoom>> GetAllReservationRoomByReservationID(string ID);
        Task<bool> CreateReservationRoom(ReservationRoom model);
        Task<bool> DeleteAllReservationRoom();
    }
}
