using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Domian.Entities.Identity;

namespace Template.Domian.Models
{
    public class Ticket : BaseModel
    {
        [Required, StringLength(500)]
        public string Content { get; set; }

        [StringLength(500)]
        public string Response { get; set; }

        [DefaultValue(false)]
        public bool IsClosed { get; set; }

        //
        //  Navigation Properties
        //

        //  User
        //[ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        //  Admin
        //[ForeignKey(nameof(Admin))]
        public Guid AdminId { get; set; }
        public virtual User Admin { get; set; }
    }
}
