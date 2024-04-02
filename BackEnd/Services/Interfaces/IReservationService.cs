using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservation();
        Task<Reservation> GetReservationByID(string id);
        Task<List<Reservation>> GetReservationByGuestID(string GuestId);

        Task<List<Reservation>> GetReservationByWasConfirm(bool isConfirm);// lấy những phiếu phòng đã xác nhận hay chưa
        Task<List<Reservation>> GetReservationByDate(DateTime StartTime, DateTime EndTime); //lấy những phiếu trong range của 2 ngày trên
        Task<bool> ReserveRooms(ReservationVM reservationvm);
        Task<bool> CreateReservation(Reservation model);
        Task<bool> UpdateReservation(Reservation model);
        Task<bool> CheckIn(string ReservationId);
        Task<bool> CheckOut(string ReservationId);
        Task<bool> Cancel(string ReservationId);
        Task<Reservation> GetReservationByRoom(string RoomId);
    }
}
