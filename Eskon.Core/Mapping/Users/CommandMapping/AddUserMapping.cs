
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Microsoft.Data.SqlClient;

namespace Eskon.Core.Mapping.Users
{
    public partial class UserProfileMapping
    {
        public void AddUserMapping()
        {
            // Source -> Destination
            CreateMap<UserRegisterDto, User>().ForMember(
                dest => dest.PasswordHash, src => src.MapFrom(src => src.Password)
                );
        }
    }
}
