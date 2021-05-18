using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class UserEmail
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
