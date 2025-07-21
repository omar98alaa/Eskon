using AutoMapper;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;

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
