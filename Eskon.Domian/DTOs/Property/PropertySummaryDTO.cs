using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Domian.DTOs.Property
{
    public class PropertySummaryDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int MaxGuests { get; set; }

        public decimal PricePerNight { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal AverageRating { get; set; }

        #region Requires Mapping
        //  City
        public string City { get; set; }

        public string Country { get; set; }
        #endregion
    }
}
