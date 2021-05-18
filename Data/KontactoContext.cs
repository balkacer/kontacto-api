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
                entity.ToTable("ADDRESS");

                entity.HasIndex(e => e.Id, "UQ__ADDRESS__3214EC260192292E")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.CityId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("CITY_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(17, 15)")
                    .HasColumnName("LATITUDE");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(18, 15)")
                    .HasColumnName("LONGITUDE");

                entity.Property(e => e.SecondAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SECOND_ADDRESS");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CITY");
            });

            modelBuilder.Entity<AddressCity>(entity =>
            {
                entity.ToTable("ADDRESS_CITY");

                entity.HasIndex(e => e.Id, "UQ__ADDRESS___3214EC2683C6175F")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AddressCities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COUNTRY");
            });

            modelBuilder.Entity<AddressCountry>(entity =>
            {
                entity.ToTable("ADDRESS_COUNTRY");

                entity.HasIndex(e => e.Id, "UQ__ADDRESS___3214EC2643574A60")
                    .IsUnique();

                entity.HasIndex(e => e.Code, "UQ__ADDRESS___AA1D4379291942CC")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__ADDRESS___D9C1FA001D9212D2")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<BusinessUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__BUSINESS__F3BEEBFF5ACB0E35");

                entity.ToTable("BUSINESS_USER");

                entity.HasIndex(e => e.UserId, "UQ__BUSINESS__F3BEEBFEBB43C184")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.AnniversaryDate)
                    .HasColumnType("date")
                    .HasColumnName("ANNIVERSARY_DATE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

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

                entity.ToTable("CONTACT");

                entity.Property(e => e.ContactId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.ContactRelationshipId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_RELATIONSHIP_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.ContactStatusId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_STATUS_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_AT");

                entity.Property(e => e.IsFavorite)
                    .HasColumnName("IS_FAVORITE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");

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
                entity.ToTable("CONTACT_RELATIONSHIP");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RELATIONSHIP");
            });

            modelBuilder.Entity<ContactRequest>(entity =>
            {
                entity.ToTable("CONTACT_REQUEST");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.IsAccepted).HasColumnName("IS_ACCEPTED");

                entity.Property(e => e.IsDenied).HasColumnName("IS_DENIED");

                entity.Property(e => e.NotificationId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("NOTIFICATION_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.ContactRequests)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NOTIFICATION_CONTACT_REQUEST");
            });

            modelBuilder.Entity<ContactShared>(entity =>
            {
                entity.ToTable("CONTACT_SHARED");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.ContactSharedId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_SHARED_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.NotificationId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("NOTIFICATION_ID")
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
                entity.ToTable("CONTACT_STATUS");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("NOTIFICATION");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_AT");

                entity.Property(e => e.NotificationStatusId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("NOTIFICATION_STATUS_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.NotificationTypeId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("NOTIFICATION_TYPE_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.ReceptorId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("RECEPTOR_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.SenderId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("SENDER_ID")
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
                entity.ToTable("NOTIFICATION_STATUS");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("NOTIFICATION_TYPE");

                entity.HasIndex(e => e.Type, "UQ__NOTIFICA__80334AA1EEBCAAAC")
                    .IsUnique();

                entity.HasIndex(e => e.Message, "UQ__NOTIFICA__819F327B7D043D53")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<PrivateUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__PRIVATE___F3BEEBFF9AD8186B");

                entity.ToTable("PRIVATE_USER");

                entity.HasIndex(e => e.UserId, "UQ__PRIVATE___F3BEEBFE901D085E")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.BusinessId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.FirstSurname)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_SURNAME");

                entity.Property(e => e.IsWorking).HasColumnName("IS_WORKING");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");

                entity.Property(e => e.Ocupation)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("OCUPATION");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("SECOND_NAME");

                entity.Property(e => e.SecondSurname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("SECOND_SURNAME");

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
                entity.ToTable("SOCIAL_MEDIA");

                entity.HasIndex(e => e.Url, "UQ__SOCIAL_M__C5B10009A0A5FF81")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__SOCIAL_M__D9C1FA004C9496FA")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.Id, "UQ__USER__3214EC266899E2BB")
                    .IsUnique();

                entity.HasIndex(e => e.PrincipalEmail, "UQ__USER__520DD7388292EDAB")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__USER__B15BE12E3E9D77D9")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.AddressId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_AT");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD")
                    .IsFixedLength(true);

                entity.Property(e => e.PrincipalEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRINCIPAL_EMAIL");

                entity.Property(e => e.UserStatusId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_STATUS_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.UserTypeId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_TYPE_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

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
                entity.ToTable("USER_EMAIL");

                entity.HasIndex(e => e.Email, "UQ__USER_EMA__161CF724891797C9")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEmails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_EMAIL");
            });

            modelBuilder.Entity<UserPhone>(entity =>
            {
                entity.ToTable("USER_PHONE");

                entity.HasIndex(e => e.Phone, "UQ__USER_PHO__D4FA0A26215857AA")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
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

                entity.ToTable("USER_SOCIAL_MEDIA");

                entity.Property(e => e.SocialMediaId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_MEDIA_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
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
                entity.ToTable("USER_STATUS");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("USER_TYPE");

                entity.HasIndex(e => e.Id, "UQ__USER_TYP__3214EC26F277A482")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}