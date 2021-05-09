using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class Contact
    {
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Nickname { get; set; }
        public bool? IsFavorite { get; set; }
        public string ContactStatusId { get; set; }
        public string ContactRelationshipId { get; set; }

        public virtual User ContactNavigation { get; set; }
        public virtual ContactRelationship ContactRelationship { get; set; }
        public virtual ContactStatus ContactStatus { get; set; }
        public virtual User User { get; set; }
    }
}
