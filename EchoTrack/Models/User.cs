namespace EchoTrack.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;  //Password should be stored as a hash
        public string Role { get; set; } = null!;
    }
}
