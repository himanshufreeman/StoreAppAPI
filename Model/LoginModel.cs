using System.ComponentModel.DataAnnotations;

namespace StoreAppAPI.Model
{
    public class LoginModel
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
