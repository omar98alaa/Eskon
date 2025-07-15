using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Domian.Entities.Identity;

namespace Template.Domian.Models
{
    [Index(nameof(Sender), nameof(ChatId), nameof(CreatedAt), IsUnique = true)]
    public class ChatMessage : BaseModel
    {
        [Required, StringLength(500)]
        public string Content { get; set; }


        //
        //  Navigation Properties
        //

        //  Sender
        //[ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }

        //  Chat
        //[ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
