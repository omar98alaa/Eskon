using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    [Index(nameof(User1), nameof(User2), IsUnique = true)]
    public class Chat : BaseModel
    {
        //
        //  Navigation Properties
        //

        //  User1
        //[ForeignKey(nameof(User1))]
        public Guid User1Id { get; set; }
        public virtual User User1 { get; set; }

        //  User2
        //[ForeignKey(nameof(User2))]
        public Guid User2Id { get; set; }
        public virtual User User2 { get; set; }

        //  Chat messages
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
