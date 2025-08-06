using Eskon.Domian.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.Models
{
    [Index(nameof(Sender), nameof(ChatId), nameof(CreatedAt), IsUnique = true)]
    public class ChatMessage : BaseModel
    {
        [Required, StringLength(500)]
        public string Content { get; set; }
       //addthis
        public bool IsRead { get; set; } = false;
        public DateTime ReadAt { get; set; }

        //
        //  Navigation Properties
        //

        //  Sender
        //[ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }
        //addthis
        public Guid ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
        //  Chat
        //[ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
