
using Eskon.Domian.DTOs.UserDTOs;
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

        public void GetAdminListMapping()
        {
            // Source -> Destination
            CreateMap<User, AdminsReadDTO>().ReverseMap();

        }
    }
}
