using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("NOTIFICATION")]
    public partial class Notification
    {
        public Notification()
        {
            ContactRequests = new HashSet<ContactRequest>();
            ContactShareds = new HashSet<ContactShared>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("NOTIFICATION_TYPE_ID")]
        [StringLength(36)]
        public string NotificationTypeId { get; set; }
        [Required]
        [Column("NOTIFICATION_STATUS_ID")]
        [StringLength(36)]
        public string NotificationStatusId { get; set; }
        [Required]
        [Column("SENDER_ID")]
        [StringLength(36)]
        public string SenderId { get; set; }
        [Required]
        [Column("RECEPTOR_ID")]
        [StringLength(36)]
        public string ReceptorId { get; set; }
        [Column("CREATED_AT", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(NotificationStatusId))]
        [InverseProperty("Notifications")]
        public virtual NotificationStatus NotificationStatus { get; set; }
        [ForeignKey(nameof(NotificationTypeId))]
        [InverseProperty("Notifications")]
        public virtual NotificationType NotificationType { get; set; }
        [ForeignKey(nameof(ReceptorId))]
        [InverseProperty(nameof(User.NotificationReceptors))]
        public virtual User Receptor { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.NotificationSenders))]
        public virtual User Sender { get; set; }
        [InverseProperty(nameof(ContactRequest.Notification))]
        public virtual ICollection<ContactRequest> ContactRequests { get; set; }
        [InverseProperty(nameof(ContactShared.Notification))]
        public virtual ICollection<ContactShared> ContactShareds { get; set; }
    }
}
