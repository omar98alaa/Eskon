using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Template.Domian.Models;
namespace Template.Domian.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }



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

        //  Transactions Out
        public virtual ICollection<Transaction> TransactionsOut { get; set; }

        //  Transactions In
        public virtual ICollection<Transaction> TransactionsIn { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }

}