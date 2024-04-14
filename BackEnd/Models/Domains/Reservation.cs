using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class Reservation
    {
        [Key]
        public string ReservationID { get; set; } = Guid.NewGuid().ToString();
        public string GuestID { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool? IsConfirmed { get; set; }
        public DateTime? ConfirmationTime { get; set; }

        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Guest? Guest { get; set; }

        [JsonIgnore]
        public virtual List<ReservationRoom>? ReservationRooms { get; set; }
    }
}
