using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.RefreshTokenDTOs
{
    public class CurrentRefreshTokenDTO
    {
        [Required]
        public string CurrentRefreshToken { get; set; }
    }
}
