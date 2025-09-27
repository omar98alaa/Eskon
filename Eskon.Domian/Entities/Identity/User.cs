using Eskon.Domian.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Eskon.Domian.Entities.Identity
{
    public class User : IdentityUser<Guid>, IBaseModel
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        public string? stripeAccountId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public string? Code { get; set; }
        [InverseProperty(nameof(UserRefreshToken.User))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();

        //
        //  Navigation Properties
        //

        //  Bookings
        public virtual ICollection<Booking> Bookings { get; set; }

        //  Chats as User1
        public virtual ICollection<Chat> User1Chats { get; set; }

        //  Chats as User2
        public virtual ICollection<Chat> User2Chats { get; set; }

        // Chat messages
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

        //  Favourites
        public virtual ICollection<Favourite> Favourites { get; set; }

        //  Notifications
        public virtual ICollection<Notification> Notifications { get; set; }

        //  Payments
        public virtual ICollection<Payment> Payments { get; set; }

        //  Assigned Properties
        public virtual ICollection<Property> AssignedProperties { get; set; }

        //  Reviews
        public virtual ICollection<Review> Reviews { get; set; }

        //  Tickets
        public virtual ICollection<Ticket> Tickets { get; set; }

        //  ResolvedTickets
        public virtual ICollection<Ticket> ResolvedTickets { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
}

}