using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class ContactRequest
    {
        public string Id { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDenied { get; set; }
        public string NotificationId { get; set; }

        public virtual Notification Notification { get; set; }
    }
}
