
using Microsoft.AspNetCore.Identity;

namespace Eskon.Domian.Entities.Identity
{
    public class UserRoles:IdentityUserRole<Guid> //, IBaseModel
    {
        //public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User {  get; set; }
        public virtual Role Role {  get; set; }

        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        //public DateTime? DeletedAt { get; set; }

    }
}
