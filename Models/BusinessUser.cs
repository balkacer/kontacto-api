using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class BusinessUser
    {
        public BusinessUser()
        {
            PrivateUsers = new HashSet<PrivateUser>();
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime AnniversaryDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PrivateUser> PrivateUsers { get; set; }
    }
}
