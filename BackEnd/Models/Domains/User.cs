using Microsoft.AspNetCore.Identity;


namespace BackEnd.Models.Domains
{
    public class User : IdentityUser
    {
        public DateTimeOffset? DateJoined { get; set; } = DateTime.Now;
    }
}
