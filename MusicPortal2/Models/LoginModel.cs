using System.ComponentModel.DataAnnotations;

namespace MusicPortal2.Models
{
    public class LoginModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
