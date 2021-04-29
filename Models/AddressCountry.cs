using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("ADDRESS_COUNTRY")]
    [Index(nameof(Id), Name = "UQ__ADDRESS___3214EC268A09E208", IsUnique = true)]
    [Index(nameof(Code), Name = "UQ__ADDRESS___AA1D437942A8E066", IsUnique = true)]
    [Index(nameof(Name), Name = "UQ__ADDRESS___D9C1FA006FAA8C05", IsUnique = true)]
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
