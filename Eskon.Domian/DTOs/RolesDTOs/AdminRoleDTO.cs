
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.RolesDTOs
{
    public class AdminRoleDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
