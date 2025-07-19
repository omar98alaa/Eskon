
namespace Eskon.Domian.DTOs.User
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
