using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class RoomType
    {
        [Key]
        public string RoomTypeID { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double DailyPrice { get; set; } 
        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<Room>? Rooms { get; set; }
    }
}
