using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class ContactStatus
    {
        public ContactStatus()
        {
            Contacts = new HashSet<Contact>();
        }

        public string Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
