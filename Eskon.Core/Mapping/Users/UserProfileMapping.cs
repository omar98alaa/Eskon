using AutoMapper;

namespace Eskon.Core.Mapping.Users
{
    public partial class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            GetUserListMapping();
            AddUserMapping();
        }

    }
}
