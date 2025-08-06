using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    [Index(nameof(CustomerId), nameof(PropertyId), IsUnique = true)]
    public class Review : BaseModel
    {
        [Required, Range(0, 5)]
        public decimal Rating { get; set; }

        [Required, StringLength(500)]
        public string Content { get; set; }

        //
        //  Navigation Properties
        //

        //  User
        //[ForeignKey(nameof(User))]
        public Guid CustomerId { get; set; }
        public virtual User Customer { get; set; }

        //  Property
        //[ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
