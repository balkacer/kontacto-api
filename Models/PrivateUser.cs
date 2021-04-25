using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("PRIVATE_USER")]
    public partial class PrivateUser
    {
        [Key]
        [Column("USER_ID")]
        [StringLength(36)]
        public string UserId { get; set; }
        [Required]
        [Column("FIRST_NAME")]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Column("SECOND_NAME")]
        [StringLength(25)]
        public string SecondName { get; set; }
        [Required]
        [Column("FIRST_SURNAME")]
        [StringLength(25)]
        public string FirstSurname { get; set; }
        [Column("SECOND_SURNAME")]
        [StringLength(25)]
        public string SecondSurname { get; set; }
        [Column("BUSINNES_ID")]
        [StringLength(36)]
        public string BusinnesId { get; set; }
        [Column("IS_WORKING")]
        public bool? IsWorking { get; set; }
        [Column("OCUPATION")]
        [StringLength(256)]
        public string Ocupation { get; set; }
        [Column("BIRTH_DATE", TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(BusinnesId))]
        [InverseProperty(nameof(BusinessUser.PrivateUsers))]
        public virtual BusinessUser Businnes { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("PrivateUser")]
        public virtual User User { get; set; }
    }
}
