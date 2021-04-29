using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("USER_SOCIAL_MEDIA")]
    public partial class UserSocialMedium
    {
        [Key]
        [Column("SOCIAL_MEDIA_ID")]
        [StringLength(36)]
        public string SocialMediaId { get; set; }
        [Required]
        [Column("USER_ID")]
        [StringLength(36)]
        public string UserId { get; set; }
        [Key]
        [Column("USERNAME")]
        [StringLength(25)]
        public string Username { get; set; }

        [ForeignKey(nameof(SocialMediaId))]
        [InverseProperty(nameof(SocialMedium.UserSocialMedia))]
        public virtual SocialMedium SocialMedia { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserSocialMedia")]
        public virtual User User { get; set; }
    }
}
