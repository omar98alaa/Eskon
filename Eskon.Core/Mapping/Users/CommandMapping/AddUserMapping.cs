
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Core.Mapping.Users
{
    public partial class UserProfileMapping
    {
        public void AddUserMapping()
        {
            // Source -> Destination
            CreateMap<UserWriteDto, User>();
        }
    }
}
