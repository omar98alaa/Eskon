using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.Models
{
    public class Image : BaseModel
    {
        [Required]
        public string Url { get; set; }

        //
        //  Navigation Properties
        //

        //  Property
        //[ForeignKey(nameof(Property))]
        public Guid? PropertyId { get; set; }
        public virtual Property? Property { get; set; }
    }
}
