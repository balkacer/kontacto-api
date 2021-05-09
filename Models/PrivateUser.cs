using System;
using System.Collections.Generic;

#nullable disable

namespace kontacto_api.Models
{
    public partial class PrivateUser
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Nickname { get; set; }
        public string BusinessId { get; set; }
        public bool IsWorking { get; set; }
        public string Ocupation { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual BusinessUser Business { get; set; }
        public virtual User User { get; set; }
    }
}
