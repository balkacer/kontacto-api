using kontacto_api.Models;
using Microsoft.EntityFrameworkCore;

namespace kontacto_api.Data
{
    public class KontactoContext: DbContext
    {
        public KontactoContext()
        {
            
        }

        public KontactoContext(DbContextOptions<KontactoContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressCity> AddressCities { get; set; }
        public virtual DbSet<AddressCountry> AddressCountries { get; set; }
        public virtual DbSet<BusinessUser> BusinessUsers { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactRelationship> ContactRelationships { get; set; }
        public virtual DbSet<ContactRequest> ContactRequests { get; set; }
        public virtual DbSet<ContactShared> ContactShareds { get; set; }
        public virtual DbSet<ContactStatus> ContactStatuses { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<PrivateUser> PrivateUsers { get; set; }
        public virtual DbSet<SocialMedium> SocialMedia { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserEmail> UserEmails { get; set; }
        public virtual DbSet<UserPhone> UserPhones { get; set; }
        public virtual DbSet<UserSocialMedium> UserSocialMedia { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
    }
}