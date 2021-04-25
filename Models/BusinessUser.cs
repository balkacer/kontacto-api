using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("BUSINESS_USER")]
    public partial class BusinessUser
    {
        public BusinessUser()
        {
            PrivateUsers = new HashSet<PrivateUser>();
        }

        [Key]
        [Column("USER_ID")]
        [StringLength(36)]
        public string UserId { get; set; }
        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("ANNIVERSARY_DATE", TypeName = "date")]
        public DateTime AnniversaryDate { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("BusinessUser")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(PrivateUser.Business))]
        public virtual ICollection<PrivateUser> PrivateUsers { get; set; }
    }
}
