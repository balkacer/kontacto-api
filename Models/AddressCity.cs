using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("ADDRESS_CITY")]
    [Index(nameof(Id), Name = "UQ__ADDRESS___3214EC26B925CA3A", IsUnique = true)]
    public partial class AddressCity
    {
        public AddressCity()
        {
            Addresses = new HashSet<Address>();
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
        [Column("COUNTRY_ID")]
        [StringLength(36)]
        public string CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty(nameof(AddressCountry.AddressCities))]
        public virtual AddressCountry Country { get; set; }
        [InverseProperty(nameof(Address.City))]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
