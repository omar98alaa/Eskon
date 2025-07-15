using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Template.Domian.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class NotificationType : BaseModel
    {
        [Required]
        public string Name { get; set; }

        //
        //  Navigation Properties
        //

        //  Notifications
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
