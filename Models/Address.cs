using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class Address
    {
        public Address()
        {
            Users = new HashSet<User>();
        }

        public string Id { get; set; }
        public string Address1 { get; set; }
        public string SecondAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string CityId { get; set; }

        public virtual AddressCity City { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
