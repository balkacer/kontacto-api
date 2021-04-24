using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("NOTIFICATION_STATUS")]
    public partial class NotificationStatus
    {
        public NotificationStatus()
        {
            Notifications = new HashSet<Notification>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("STATUS")]
        [StringLength(25)]
        public string Status { get; set; }

        [InverseProperty(nameof(Notification.NotificationStatus))]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
