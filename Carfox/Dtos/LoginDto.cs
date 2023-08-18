using System.ComponentModel.DataAnnotations;

namespace Carfox.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}
