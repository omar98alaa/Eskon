using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Template.Domian.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Country : BaseModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        //
        //  Navigation Properties
        //

        //  Cities
        public virtual ICollection<City> Cities { get; set; }
    }
}
