
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
        [PasswordPropertyText, MinLength(8)]
        public string Password { get; set; }

        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\+?[\d\s\-().]{7,20}$",
    ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
