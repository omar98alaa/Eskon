
using Template.Domian.DTOs.User;
using Template.Domian.Entities.Identity;

namespace Template.Core.Mapping.Users
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
