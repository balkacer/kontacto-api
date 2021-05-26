using System;

namespace kontacto_api.DTO
{
    
    public class PrivateUserDTO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        #nullable enable
        public string? WorkName { get; set; }
        public bool? IsWorking { get; set; }
        #nullable disable
        public string Occupation { get; set; }
        public string BirthDate { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string PrincipalEmail { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public AddressDTO Address { get; set; }
    }

    public class GetPrivateUserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        #nullable enable
        public string? WorkName { get; set; }
        public bool? IsWorking { get; set; }
        #nullable disable
        public string Occupation { get; set; }
        public string BirthDate { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string PrincipalEmail { get; set; }
        public string UserType { get; set; }
        public AddressDTO Address { get; set; }
    }
}