
using Microsoft.AspNetCore.Identity;

namespace Eskon.Domian.Entities.Identity
{
    public class UserRoles:IdentityUserRole<Guid>
    {
        public virtual User User {  get; set; }
        public virtual Role Role {  get; set; }
    }
}
