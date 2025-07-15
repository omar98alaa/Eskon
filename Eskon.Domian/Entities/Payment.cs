using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    public class Payment : BaseModel
    {
        [Required, Range(0.0, double.MaxValue)]
        public decimal Amount { get; set; }

        [DefaultValue(false)]
        public bool IsSuccessful { get; set; }


        //
        //  Navigation Properties
        //

        //  User
        //[ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

    }
}
