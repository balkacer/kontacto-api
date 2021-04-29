using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("ADDRESS")]
    [Index(nameof(Id), Name = "UQ__ADDRESS__3214EC265F4F2A34", IsUnique = true)]
    public partial class Address
    {
        public Address()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("ADDRESS")]
        [StringLength(100)]
        public string Address1 { get; set; }
        [Column("SECOND_ADDRESS")]
        [StringLength(100)]
        public string SecondAddress { get; set; }
        [Column("LATITUDE", TypeName = "decimal(17, 15)")]
        public decimal Latitude { get; set; }
        [Column("LONGITUDE", TypeName = "decimal(18, 15)")]
        public decimal Longitude { get; set; }
        [Required]
        [Column("CITY_ID")]
        [StringLength(36)]
        public string CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty(nameof(AddressCity.Addresses))]
        public virtual AddressCity City { get; set; }
        [InverseProperty(nameof(User.Address))]
        public virtual ICollection<User> Users { get; set; }
    }
}
