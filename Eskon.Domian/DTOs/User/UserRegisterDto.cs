
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.User
{
    public class UserRegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression("^\\+?[1-9]\\d{1,14}$")]
        public string PhoneNumber { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }
    }
}
