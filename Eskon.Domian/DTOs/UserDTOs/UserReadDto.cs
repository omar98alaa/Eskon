
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.UserDTOs
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string phoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
