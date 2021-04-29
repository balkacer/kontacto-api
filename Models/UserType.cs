using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("USER_TYPE")]
    [Index(nameof(Id), Name = "UQ__USER_TYP__3214EC2695261587", IsUnique = true)]
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("TYPE")]
        [StringLength(25)]
        public string Type { get; set; }

        [InverseProperty(nameof(User.UserType))]
        public virtual ICollection<User> Users { get; set; }
    }
}
