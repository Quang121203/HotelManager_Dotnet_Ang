using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class Bill
    {
        [Key]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public double Sum { get; set; }
        public bool Status { get; set; }
        public string IDGuest { get; set; }

        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Guest? Guest { get; set; }
    }
}
