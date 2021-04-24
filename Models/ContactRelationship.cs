using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("CONTACT_RELATIONSHIP")]
    public partial class ContactRelationship
    {
        public ContactRelationship()
        {
            Contacts = new HashSet<Contact>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("RELATIONSHIP")]
        [StringLength(50)]
        public string Relationship { get; set; }

        [InverseProperty(nameof(Contact.ContactRelationship))]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
