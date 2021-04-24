using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("USER_EMAIL")]
    [Index(nameof(Email), Name = "UQ__USER_EMA__161CF724BA05FD51", IsUnique = true)]
    public partial class UserEmail
    {
        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("EMAIL")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("USER_ID")]
        [StringLength(36)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserEmails")]
        public virtual User User { get; set; }
    }
}
