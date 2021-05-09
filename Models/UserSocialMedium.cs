using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class UserSocialMedium
    {
        public string SocialMediaId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }

        public virtual SocialMedium SocialMedia { get; set; }
        public virtual User User { get; set; }
    }
}
