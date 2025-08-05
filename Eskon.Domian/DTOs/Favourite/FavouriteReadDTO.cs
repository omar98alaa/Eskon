using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Domian.DTOs.Favourite
{
    public class FavouriteReadDTO
    {
        public Guid UserId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid PropertyTitle { get; set; }
    }
}
