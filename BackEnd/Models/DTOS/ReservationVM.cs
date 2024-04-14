using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.DTOS
{
    public class ReservationVM
    {
        public string GuestFullName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string GuestEmail { get; set; }
        public string RoomTypeId { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;
        public int NumberOfRooms { get; set; } = 1;

    }
}
