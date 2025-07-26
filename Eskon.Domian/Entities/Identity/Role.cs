using Microsoft.AspNetCore.Identity;

namespace Eskon.Domian.Entities.Identity
{
    public class Role : IdentityRole<Guid>, IBaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
