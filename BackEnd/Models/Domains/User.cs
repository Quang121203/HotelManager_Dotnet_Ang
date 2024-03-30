using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Domains
{
    public class User
    {
        [Key]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? DateJoined { get; set; } = DateTime.Now;
    }
}
