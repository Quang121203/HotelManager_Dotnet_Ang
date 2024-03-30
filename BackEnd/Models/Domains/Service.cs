using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class Service
    {
        [Key]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTime.Now;


        [JsonIgnore]
        public ICollection<GuestService>? GuestService { get; set; }
    }
}
