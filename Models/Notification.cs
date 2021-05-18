using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class Notification
    {
        public Notification()
        {
            ContactRequests = new HashSet<ContactRequest>();
            ContactShareds = new HashSet<ContactShared>();
        }

        public string Id { get; set; }
        public string NotificationTypeId { get; set; }
        public string NotificationStatusId { get; set; }
        public string SenderId { get; set; }
        public string ReceptorId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual NotificationStatus NotificationStatus { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public virtual User Receptor { get; set; }
        public virtual User Sender { get; set; }
        public virtual ICollection<ContactRequest> ContactRequests { get; set; }
        public virtual ICollection<ContactShared> ContactShareds { get; set; }
    }
}
