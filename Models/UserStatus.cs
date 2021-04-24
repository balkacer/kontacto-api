using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("USER_STATUS")]
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("STATUS")]
        [StringLength(25)]
        public string Status { get; set; }

        [InverseProperty(nameof(User.UserStatus))]
        public virtual ICollection<User> Users { get; set; }
    }
}
