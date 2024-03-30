using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class ReservationRoom
    {
        public string RoomID { get; set; }
        public string ReservationID { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;


        // Navigation properties
        [JsonIgnore]
        public Room Room { get; set; }
        [JsonIgnore]
        public Reservation Reservation { get; set; }

    }
}
