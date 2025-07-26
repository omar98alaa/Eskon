
namespace Eskon.Domian.DTOs.User
{
    public class AdminsReadDTO
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
