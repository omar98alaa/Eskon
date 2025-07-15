using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Template.Domian.Entities.Identity;
using Template.Domian.Models;
namespace Template.Infrastructure.Context
{
    public class MyDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        // To migrate
        //Add-Migration InitialCreate -OutputDir Migrations/

        // Update Database
        // Update-Database
        #region Constructors
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion

        #region Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Booking
            //  Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Chat
            // Chat
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User1)
                .WithMany(u => u.User1Chats)
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany(u => u.User2Chats)
                .HasForeignKey(c => c.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region ChatMessage
            // Chatmessage
            modelBuilder.Entity<ChatMessage>()
                .HasOne(chm => chm.Sender)
                .WithMany(u => u.ChatMessages)
                .HasForeignKey(chm => chm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(chm => chm.Chat)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(chm => chm.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region City
            // City
            modelBuilder.Entity<City>()
                .HasOne(cty => cty.Country)
                .WithMany(cntry => cntry.Cities)
                .HasForeignKey(cty => cty.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Favourite
            // Favourite
            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Property)
                .WithMany(p => p.Favourites)
                .HasForeignKey(f => f.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Entity
            // Image
            modelBuilder.Entity<Image>()
                .HasOne(I => I.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(I => I.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Notification
            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.NotificationType)
                .WithMany(nt => nt.Notifications)
                .HasForeignKey(n => n.NotificationTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Payment
            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Property
            // Property
            modelBuilder.Entity<Property>()
                .HasOne(p => p.PropertyType)
                .WithMany(pt => pt.Properties)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.City)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.AssignedAdmin)
                .WithMany(u => u.AssignedProperties)
                .HasForeignKey(p => p.AssignedAdminId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Review
            // Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Property)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Ticket
            // Ticket 
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Admin)
                .WithMany(u => u.ResolvedTickets)
                .HasForeignKey(t => t.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Transactions
            // Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Sender)
                .WithMany(u => u.TransactionsOut)
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Receiver)
                .WithMany(u => u.TransactionsIn)
                .HasForeignKey(t => t.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Identity
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
            #endregion

        }

        //
        //  Override save changes
        //

        public override int SaveChanges()
        {
            SetTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseModel &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(BaseModel.CreatedAt)).CurrentValue = now;
                }

                entry.Property(nameof(BaseModel.UpdatedAt)).CurrentValue = now;
            }
        }
        #endregion
    }
}
