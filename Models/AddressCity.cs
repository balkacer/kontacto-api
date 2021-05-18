using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class AddressCity
    {
        public AddressCity()
        {
            Addresses = new HashSet<Address>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }

        public virtual AddressCountry Country { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
