using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class ContactRelationship
    {
        public ContactRelationship()
        {
            Contacts = new HashSet<Contact>();
        }

        public string Id { get; set; }
        public string Relationship { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
