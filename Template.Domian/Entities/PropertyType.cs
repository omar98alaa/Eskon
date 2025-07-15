using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Template.Domian.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class PropertyType : BaseModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        //
        //  Navigation Properties
        //

        //  Properties
        public virtual ICollection<Property> Properties { get; set; }
    }
}
