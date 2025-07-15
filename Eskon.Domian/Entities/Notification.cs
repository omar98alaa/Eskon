using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    public class Notification : BaseModel
    {
        public Guid RedirectionId { get; set; }

        [Required, StringLength(200)]
        public string Content { get; set; }

        [DefaultValue(false)]
        public bool IsRead { get; set; }

        //
        //  Navigation Property
        //

        //  Receiver
        //[ForeignKey(nameof(Receiver))]
        public Guid ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        //  Notification Type
        //[ForeignKey(nameof(NotificationType))]
        public Guid NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
