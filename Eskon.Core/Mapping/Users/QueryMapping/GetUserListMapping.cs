
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Core.Mapping.Users
{
    public partial class UserProfileMapping
    {
        public void GetUserListMapping()
        {
            // Source -> Destination
            CreateMap<User, UserReadDto>();

        }
    }
}
