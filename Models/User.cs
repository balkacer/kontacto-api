using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
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

        public string Id { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string PrincipalEmail { get; set; }
        public string Password { get; set; }
        public string UserTypeId { get; set; }
        public string UserStatusId { get; set; }
        public string AddressId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Address Address { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual BusinessUser BusinessUser { get; set; }
        public virtual PrivateUser PrivateUser { get; set; }
        public virtual ICollection<Contact> ContactContactNavigations { get; set; }
        public virtual ICollection<ContactShared> ContactShareds { get; set; }
        public virtual ICollection<Contact> ContactUsers { get; set; }
        public virtual ICollection<Notification> NotificationReceptors { get; set; }
        public virtual ICollection<Notification> NotificationSenders { get; set; }
        public virtual ICollection<UserEmail> UserEmails { get; set; }
        public virtual ICollection<UserPhone> UserPhones { get; set; }
        public virtual ICollection<UserSocialMedium> UserSocialMedia { get; set; }
    }
}
