
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.Roles
{
    public class AdminRoleDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
