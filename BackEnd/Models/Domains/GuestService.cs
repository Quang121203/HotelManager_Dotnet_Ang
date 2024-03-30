using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class GuestService
    {
        public string GuestID { get; set; }
        public string ServiceID { get; set; }
        public int Number { get; set; }

        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;

        // Navigation properties
        [JsonIgnore]
        public Guest Guest { get; set; }
        [JsonIgnore]
        public Service Service { get; set; }

        
    }
}
