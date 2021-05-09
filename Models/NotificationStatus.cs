using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class NotificationStatus
    {
        public NotificationStatus()
        {
            Notifications = new HashSet<Notification>();
        }

        public string Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
