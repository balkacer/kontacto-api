using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("USER")]
    [Index(nameof(Id), Name = "UQ__USER__3214EC26A6384743", IsUnique = true)]
    [Index(nameof(PrincipalEmail), Name = "UQ__USER__520DD73827A936D7", IsUnique = true)]
    [Index(nameof(Username), Name = "UQ__USER__B15BE12E9D15F498", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            ContactContactNavigations = new HashSet<Contact>();
            ContactShareds = new HashSet<ContactShared>();
            ContactUsers = new HashSet<Contact>();
            NotificationReceptors = new HashSet<Notification>();
            NotificationSenders = new HashSet<Notification>();
            UserEmails = new HashSet<UserEmail>();
            UserPhones = new HashSet<UserPhone>();
            UserSocialMedia = new HashSet<UserSocialMedium>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Column("IMAGE")]
        public string Image { get; set; }
        [Required]
        [Column("USERNAME")]
        [StringLength(25)]
        public string Username { get; set; }
        [Column("NICKNAME")]
        [StringLength(25)]
        public string Nickname { get; set; }
        [Required]
        [Column("PRINCIPAL_EMAIL")]
        [StringLength(50)]
        public string PrincipalEmail { get; set; }
        [Required]
        [Column("PASSWORD")]
        [StringLength(64)]
        public string Password { get; set; }
        [Required]
        [Column("USER_TYPE_ID")]
        [StringLength(36)]
        public string UserTypeId { get; set; }
        [Required]
        [Column("USER_STATUS_ID")]
        [StringLength(36)]
        public string UserStatusId { get; set; }
        [Required]
        [Column("ADDRESS_ID")]
        [StringLength(36)]
        public string AddressId { get; set; }
        [Column("CREATED_AT", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column("LAST_UPADE", TypeName = "datetime")]
        public DateTime LastUpade { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Users")]
        public virtual Address Address { get; set; }
        [ForeignKey(nameof(UserStatusId))]
        [InverseProperty("Users")]
        public virtual UserStatus UserStatus { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("Users")]
        public virtual UserType UserType { get; set; }
        [InverseProperty("User")]
        public virtual BusinessUser BusinessUser { get; set; }
        [InverseProperty("User")]
        public virtual PrivateUser PrivateUser { get; set; }
        [InverseProperty(nameof(Contact.ContactNavigation))]
        public virtual ICollection<Contact> ContactContactNavigations { get; set; }
        [InverseProperty(nameof(ContactShared.ContactSharedNavigation))]
        public virtual ICollection<ContactShared> ContactShareds { get; set; }
        [InverseProperty(nameof(Contact.User))]
        public virtual ICollection<Contact> ContactUsers { get; set; }
        [InverseProperty(nameof(Notification.Receptor))]
        public virtual ICollection<Notification> NotificationReceptors { get; set; }
        [InverseProperty(nameof(Notification.Sender))]
        public virtual ICollection<Notification> NotificationSenders { get; set; }
        [InverseProperty(nameof(UserEmail.User))]
        public virtual ICollection<UserEmail> UserEmails { get; set; }
        [InverseProperty(nameof(UserPhone.User))]
        public virtual ICollection<UserPhone> UserPhones { get; set; }
        [InverseProperty(nameof(UserSocialMedium.User))]
        public virtual ICollection<UserSocialMedium> UserSocialMedia { get; set; }
    }
}
