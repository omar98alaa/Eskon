using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Domian.Models
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
        public Guid PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
