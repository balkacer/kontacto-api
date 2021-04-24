using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("CONTACT_REQUEST")]
    public partial class ContactRequest
    {
        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Column("IS_ACCEPTED")]
        public bool? IsAccepted { get; set; }
        [Column("IS_DENIED")]
        public bool? IsDenied { get; set; }
        [Required]
        [Column("NOTIFICATION_ID")]
        [StringLength(36)]
        public string NotificationId { get; set; }

        [ForeignKey(nameof(NotificationId))]
        [InverseProperty("ContactRequests")]
        public virtual Notification Notification { get; set; }
    }
}
