using System;

namespace kontacto_api.DTO
{
    public class PrivateUserDTO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string BusinnesId { get; set; }
        public bool? IsWorking { get; set; }
        public string Ocupation { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string PrincipalEmail { get; set; }
        public string Password { get; set; }
        public string UserTypeId { get; set; }
        public string UserStatusId { get; set; }
        public string AddressId { get; set; }
    }

    public class GetPrivateUserDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string BusinnesId { get; set; }
        public bool? IsWorking { get; set; }
        public string Ocupation { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string PrincipalEmail { get; set; }
        public string UserTypeId { get; set; }
        public string UserStatusId { get; set; }
        public string AddressId { get; set; }
    }
}