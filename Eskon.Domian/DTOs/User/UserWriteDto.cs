
namespace Eskon.Domian.DTOs.User
{
    public class UserWriteDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
