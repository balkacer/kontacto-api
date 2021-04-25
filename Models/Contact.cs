using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("CONTACT")]
    public partial class Contact
    {
        [Key]
        [Column("CONTACT_ID")]
        [StringLength(36)]
        public string ContactId { get; set; }
        // [Key]
        [Column("USER_ID")]
        [StringLength(36)]
        public string UserId { get; set; }
        [Column("CREATED_AT", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("NICKNAME")]
        [StringLength(25)]
        public string Nickname { get; set; }
        [Column("IS_FAVORITE")]
        public bool? IsFavorite { get; set; }
        [Required]
        [Column("CONTACT_STATUS_ID")]
        [StringLength(36)]
        public string ContactStatusId { get; set; }
        [Required]
        [Column("CONTACT_RELATIONSHIP_ID")]
        [StringLength(36)]
        public string ContactRelationshipId { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("ContactContactNavigations")]
        public virtual User ContactNavigation { get; set; }
        [ForeignKey(nameof(ContactRelationshipId))]
        [InverseProperty("Contacts")]
        public virtual ContactRelationship ContactRelationship { get; set; }
        [ForeignKey(nameof(ContactStatusId))]
        [InverseProperty("Contacts")]
        public virtual ContactStatus ContactStatus { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ContactUsers")]
        public virtual User User { get; set; }
    }
}
