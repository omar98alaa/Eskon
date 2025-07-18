using Eskon.Domian.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eskon.Domian.Entities.Identity
{
    public class UserRefreshToken : BaseModel
    {
        public Guid UserId { get; set; }
        public string? RefreshToken { get; set; } = default!;
        public bool IsRevoked { get; set; } = false;
        public DateTime ExpiresAt { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.UserRefreshTokens))]
        public virtual User User { get; set; } = default!;
    }
}
