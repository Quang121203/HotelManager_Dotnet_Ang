using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class Guest
    {
        [Key]
        public string GuestID { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<Reservation>? Reservations { get; set; }
        [JsonIgnore]
        public ICollection<GuestService>? GuestService { get; set; }
        [JsonIgnore]
        public ICollection<Bill>? Bills { get; set; }
    }
}
