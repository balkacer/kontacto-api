using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("USER_PHONE")]
    public partial class UserPhone
    {
        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("PHONE")]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [Column("USER_ID")]
        [StringLength(36)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserPhones")]
        public virtual User User { get; set; }
    }
}
