using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("ADDRESS_COUNTRY")]
    public partial class AddressCountry
    {
        public AddressCountry()
        {
            AddressCities = new HashSet<AddressCity>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("CODE")]
        [StringLength(3)]
        public string Code { get; set; }
        [Required]
        [Column("NAME")]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(AddressCity.Country))]
        public virtual ICollection<AddressCity> AddressCities { get; set; }
    }
}
