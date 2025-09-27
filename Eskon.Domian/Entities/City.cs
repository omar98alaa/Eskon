using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.Models
{
    public class City : BaseModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        //
        //  Navigation Properties
        //

        //  Country
        //[ForeignKey(nameof(Country))]
        public Guid CountryId { get; set; }

        //  Properties
        public virtual Country Country { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
