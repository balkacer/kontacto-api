using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notification>();
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
