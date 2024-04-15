using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models.Domains
{
    public class Token
    {
        [Key]
        public string RefeshToken { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresRefeshToken { get; set; }

    }
}
