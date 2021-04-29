using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("SOCIAL_MEDIA")]
    [Index(nameof(Url), Name = "UQ__SOCIAL_M__C5B100098FA01D7F", IsUnique = true)]
    [Index(nameof(Name), Name = "UQ__SOCIAL_M__D9C1FA00BCB61072", IsUnique = true)]
    public partial class SocialMedium
    {
        public SocialMedium()
        {
            UserSocialMedia = new HashSet<UserSocialMedium>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("NAME")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("URL")]
        [StringLength(250)]
        public string Url { get; set; }

        [InverseProperty(nameof(UserSocialMedium.SocialMedia))]
        public virtual ICollection<UserSocialMedium> UserSocialMedia { get; set; }
    }
}
