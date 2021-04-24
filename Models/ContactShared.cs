using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("CONTACT_SHARED")]
    public partial class ContactShared
    {
        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("CONTACT_SHARED_ID")]
        [StringLength(36)]
        public string ContactSharedId { get; set; }
        [Required]
        [Column("NOTIFICATION_ID")]
        [StringLength(36)]
        public string NotificationId { get; set; }

        [ForeignKey(nameof(ContactSharedId))]
        [InverseProperty(nameof(User.ContactShareds))]
        public virtual User ContactSharedNavigation { get; set; }
        [ForeignKey(nameof(NotificationId))]
        [InverseProperty("ContactShareds")]
        public virtual Notification Notification { get; set; }
    }
}
