using System;

namespace kontacto_api.DTO
{
    
    public class BusinessUserDTO
    {
        public string Name { get; set;}
        public string AnniversaryDate {get; set;}
        public string Image { get; set; }
        public string Username { get; set; }
        public string PrincipalEmail { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public AddressDTO Address { get; set; }
    }

    public class GetBusinessUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set;}
        public string AnniversaryDate {get; set;}
        public string Image { get; set; }
        public string Username { get; set; }
        public string PrincipalEmail { get; set; }
        public string UserType { get; set; }
        public AddressDTO Address { get; set; }
    }
}