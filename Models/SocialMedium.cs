using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class SocialMedium
    {
        public SocialMedium()
        {
            UserSocialMedia = new HashSet<UserSocialMedium>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<UserSocialMedium> UserSocialMedia { get; set; }
    }
}
