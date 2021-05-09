using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class AddressCountry
    {
        public AddressCountry()
        {
            AddressCities = new HashSet<AddressCity>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AddressCity> AddressCities { get; set; }
    }
}
