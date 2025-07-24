using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.RefreshToken
{
    public class CurrentRefreshTokenDTO
    {
        [Required]
        public string CurrentRefreshToken { get; set; }
    }
}
