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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.CityId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SecondAddress).IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CITY");
            });

            modelBuilder.Entity<AddressCity>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CountryId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AddressCities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COUNTRY");
            });

            modelBuilder.Entity<AddressCountry>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<BusinessUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__BUSINESS__F3BEEBFFA6A79E92");

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.BusinessUser)
                    .HasForeignKey<BusinessUser>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_USER");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.UserId })
                    .HasName("PK_USER_CONTACT");

                entity.Property(e => e.ContactId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ContactRelationshipId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ContactStatusId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsFavorite).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nickname).IsUnicode(false);

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.ContactContactNavigations)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONTACT");

                entity.HasOne(d => d.ContactRelationship)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactRelationshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONTACT_RELATIONSHIP");

                entity.HasOne(d => d.ContactStatus)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONTACT_STATUS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContactUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONTACTER");
            });

            modelBuilder.Entity<ContactRelationship>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Relationship).IsUnicode(false);
            });

            modelBuilder.Entity<ContactRequest>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NotificationId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.ContactRequests)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NOTIFICATION_CONTACT_REQUEST");
            });

            modelBuilder.Entity<ContactShared>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ContactSharedId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NotificationId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.ContactSharedNavigation)
                    .WithMany(p => p.ContactShareds)
                    .HasForeignKey(d => d.ContactSharedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONTACT_SHARED");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.ContactShareds)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NOTIFICATION_CONTACT_SHARED");
            });

            modelBuilder.Entity<ContactStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Status).IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.NotificationStatusId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NotificationTypeId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ReceptorId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SenderId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.NotificationStatus)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NOTIFICATION_STATUS_ID");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NOTIFICATION_TYPE");

                entity.HasOne(d => d.Receptor)
                    .WithMany(p => p.NotificationReceptors)
                    .HasForeignKey(d => d.ReceptorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_RECEPTOR");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.NotificationSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_SENDER");
            });

            modelBuilder.Entity<NotificationStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Status).IsUnicode(false);
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<PrivateUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__PRIVATE___F3BEEBFF56EF38D7");

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.BusinessId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.FirstSurname).IsUnicode(false);

                entity.Property(e => e.IsWorking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ocupation).IsUnicode(false);

                entity.Property(e => e.SecondName).IsUnicode(false);

                entity.Property(e => e.SecondSurname).IsUnicode(false);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.PrivateUsers)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK_WORK");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.PrivateUser)
                    .HasForeignKey<PrivateUser>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRIVATE_USER");
            });

            modelBuilder.Entity<SocialMedium>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AddressId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Nickname).IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PrincipalEmail).IsUnicode(false);

                entity.Property(e => e.UserStatusId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserTypeId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADDRESS");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_STATUS");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_TYPE");
            });

            modelBuilder.Entity<UserEmail>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEmails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_EMAIL");
            });

            modelBuilder.Entity<UserPhone>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPhones)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_PHONE");
            });

            modelBuilder.Entity<UserSocialMedium>(entity =>
            {
                entity.HasKey(e => new { e.SocialMediaId, e.Username })
                    .HasName("PK_USER_SOCIAL_MEDIA_NICKNAME");

                entity.Property(e => e.SocialMediaId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SocialMedia)
                    .WithMany(p => p.UserSocialMedia)
                    .HasForeignKey(d => d.SocialMediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOCIAL_MEDIA");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSocialMedia)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Status).IsUnicode(false);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Type).IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}