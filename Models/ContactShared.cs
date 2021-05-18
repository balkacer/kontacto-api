using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class ContactShared
    {
        public string Id { get; set; }
        public string ContactSharedId { get; set; }
        public string NotificationId { get; set; }

        public virtual User ContactSharedNavigation { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
