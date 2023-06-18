using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PhoneBookWpf.Models
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
