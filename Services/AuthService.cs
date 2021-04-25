using System;
using System.Linq;
using System.Threading.Tasks;
using kontacto_api.Data;
using kontacto_api.DTO;
using kontacto_api.Models;
using Microsoft.EntityFrameworkCore;

namespace kontacto_api.Services
{
    public class AuthService
    {
        private readonly KontactoContext _context;
        public AuthService(KontactoContext context)
        {
            _context = context;
        }

        private async Task<GetPrivateUserDTO> GetPrivateUserDTOAsync(string id) {
            var user = await _context.Users
                .Include(s => s.UserStatus)
                .Include(t => t.UserType)
                .Include(a => a.Address)
                .Include(b => b.BusinessUser)
                .FirstOrDefaultAsync(u => u.Id == id);

            var city = await _context.AddressCities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(ct => ct.Id == user.Address.CityId);

            var pUser = await _context.PrivateUsers.FindAsync(id);

            var addresObj = new AddressDTO {
                Address = user.Address.Address1,
                SecondAddress = user.Address.SecondAddress,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
                City = city.Name,
                Country = city.Country.Name,
                CountryCode = city.Country.Code
            };

            var pUserDTO = new GetPrivateUserDTO {
                Id = user.Id,
                FirstName = pUser.FirstName,
                SecondName = pUser.SecondName,
                FirstSurname = pUser.FirstSurname,
                SecondSurname = pUser.SecondSurname,
                WorkName = user.BusinessUser?.Name,
                IsWorking = pUser.IsWorking,
                Ocupation = pUser.Ocupation,
                BirthDate = pUser.BirthDate.ToString("dd/MM/yyyy"),
                Image = null,
                Username = user.Username,
                Nickname = user.Nickname,
                PrincipalEmail = user.PrincipalEmail,
                UserType = user.UserType.Type,
                UserStatus = user.UserStatus.Status,
                Address = addresObj
            };

            return pUserDTO;
        }

        private async Task<GetPrivateUserDTO> GetBusinessUserDTOAsync(string id) {
            var user = await _context.Users.FindAsync(id);
            var pUser = await _context.PrivateUsers.FindAsync(id);

            var addresObj = new AddressDTO {
                Address = user.Address.Address1,
                SecondAddress = user.Address.SecondAddress,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
                City = user.Address.City.Name,
                Country = user.Address.City.Country.Name,
                CountryCode = user.Address.City.Country.Code
            };

            var pUserDTO = new GetPrivateUserDTO {
                Id = user.Id,
                FirstName = pUser.FirstName,
                SecondName = pUser.SecondName,
                FirstSurname = pUser.FirstSurname,
                SecondSurname = pUser.SecondSurname,
                WorkName = user.BusinessUser.Name,
                IsWorking = pUser.IsWorking,
                Ocupation = pUser.Ocupation,
                BirthDate = pUser.BirthDate.ToString("dd/MM/yyyy"),
                Image = "",
                Username = user.Username,
                Nickname = user.Nickname,
                PrincipalEmail = user.PrincipalEmail,
                UserType = user.UserType.Type,
                UserStatus = user.UserStatus.Status,
                Address = addresObj
            };

            return pUserDTO;
        }

        public async Task<Object> GetUserAsync(string id) {
            var userType = await this.GetUserTypeAsync(id);
            if (userType == "PRIVATE") {
                var pUser = await this.GetPrivateUserDTOAsync(id);
                return pUser;
            }

            var bUser = await this.GetBusinessUserDTOAsync(id);
            return bUser;
        }

        private async Task<String> GetUserTypeAsync(string id) {
            var user = await _context.Users.FindAsync(id);
            var userType = await _context.UserTypes.FindAsync(user.UserTypeId);
            return userType.Type;
        }

        private async Task<User> CreateNewUserAsync(User user) {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<PrivateUser> CreateNewPrivateUserAsync(PrivateUserDTO pUserDTO) {
            var userType = await _context.UserTypes.Where(t => t.Type == pUserDTO.UserType).FirstOrDefaultAsync();
            var userStatus = await _context.UserStatuses.Where(s => s.Status == pUserDTO.UserStatus).FirstOrDefaultAsync();
            var address = await _context.Addresses.Where(a => a.Address1 == pUserDTO.Address.Address).FirstOrDefaultAsync();
            var business = await _context.BusinessUsers.Where(b => b.Name == pUserDTO.WorkName).FirstOrDefaultAsync();

            var user = new User {
                Id = Guid.NewGuid().ToString(),
                Image = null,
                Username = pUserDTO.Username,
                Nickname = pUserDTO.Nickname,
                PrincipalEmail = pUserDTO.PrincipalEmail,
                Password = pUserDTO.Password,
                UserTypeId = userType.Id,
                UserStatusId = userStatus.Id,
                AddressId = address.Id,
                LastUpade = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            user = await this.CreateNewUserAsync(user);

            var pUser = new PrivateUser {
                UserId = user.Id,
                FirstName = pUserDTO.FirstName,
                SecondName = pUserDTO.SecondName,
                FirstSurname = pUserDTO.FirstSurname,
                SecondSurname = pUserDTO.SecondSurname,
                BusinessId = null,
                IsWorking = pUserDTO.IsWorking,
                Ocupation = pUserDTO.Ocupation,
                BirthDate = DateTime.Parse(pUserDTO.BirthDate)
            };

            await _context.PrivateUsers.AddAsync(pUser);
            await _context.SaveChangesAsync();

            return pUser;
        }
    }
}