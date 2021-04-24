using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kontacto_api.Models
{
    [Table("CONTACT_STATUS")]
    public partial class ContactStatus
    {
        public ContactStatus()
        {
            Contacts = new HashSet<Contact>();
        }

        [Key]
        [Column("ID")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("STATUS")]
        [StringLength(25)]
        public string Status { get; set; }

        [InverseProperty(nameof(Contact.ContactStatus))]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
