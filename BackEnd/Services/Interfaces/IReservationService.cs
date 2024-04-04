using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IReservationService
    {
        Task<object> GetAllReservation();
        Task<object> GetReservationByID(string id);
        Task<object> GetReservationByGuestID(string GuestId);

        Task<object> GetReservationByWasConfirm(bool isConfirm);// lấy những phiếu phòng đã xác nhận hay chưa
        Task<List<Reservation>> GetReservationByDate(DateTime StartTime, DateTime EndTime); //lấy những phiếu trong range của 2 ngày trên
        Task<object> ReserveRooms(ReservationVM reservationvm);
        Task<object> CreateReservation(Reservation model);
        Task<object> UpdateReservation(Reservation model);
        Task<object> CheckIn(string ReservationId);
        Task<object> CheckOut(string ReservationId);
        Task<object> Cancel(string ReservationId);
        Task<Reservation> GetReservationByRoom(string RoomId);
        Task<object> DeleteReservation(string id);
    }
}
