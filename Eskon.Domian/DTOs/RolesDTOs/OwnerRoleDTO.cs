
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.RolesDTOs
{
    public class OwnerRoleDTO
    {
        [Required]
        [Url]
        public string RefreshUrl { get; set; }

        [Required]
        [Url]
        public string ReturnUrl { get; set; }

    }
}
