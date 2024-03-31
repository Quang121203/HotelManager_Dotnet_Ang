using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class Room
    {
        [Key]
        public string RoomID { get; set; } = Guid.NewGuid().ToString();
        public string RoomNumber { get; set; }
        public string RoomTypeID { get; set; }
        public bool IsAvaiable { get; set; } // Avaiable true là rỗng
        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;

        [JsonIgnore]
        public RoomType? RoomType { get; set; }
        [JsonIgnore]
        public ICollection<ReservationRoom>? ReservationRooms { get; set; }
    }
}
