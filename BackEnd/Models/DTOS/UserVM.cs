namespace BackEnd.Models.DTOS
{
    public class UserVM
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; } = "";
    }
}
