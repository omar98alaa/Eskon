
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.User
{
    public class UserSignInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText, MinLength(8)]
        public string Password { get; set; }
    }
}
