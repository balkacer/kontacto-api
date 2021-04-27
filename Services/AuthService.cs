using System;
using System.Linq;
using System.Threading.Tasks;
using kontacto_api.Data;
using kontacto_api.DTO;
using kontacto_api.Models;
using kontacto_api.Tools;
using kontacto_api.Tools.Enums;
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

        private async Task<GetBusinessUserDTO> GetBusinessUserDTOAsync(string id) {

            var user = await _context.Users
                .Include(s => s.UserStatus)
                .Include(t => t.UserType)
                .Include(a => a.Address)
                .FirstOrDefaultAsync(u => u.Id == id);

            var city = await _context.AddressCities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(ct => ct.Id == user.Address.CityId);

            var bUser = await _context.BusinessUsers.FindAsync(id);

            var addresObj = new AddressDTO
            {
                Address = user.Address.Address1,
                SecondAddress = user.Address.SecondAddress,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
                City = city.Name,
                Country = city.Country.Name,
                CountryCode = city.Country.Code
            };

            var bUserDTO = new GetBusinessUserDTO {
                Id = user.Id,
                Name = bUser.Name,
                AnniversaryDate = bUser.AnniversaryDate.ToString("dd/MM/yyyy"),
                Image = "",
                Username = user.Username,
                Nickname = user.Nickname,
                PrincipalEmail = user.PrincipalEmail,
                UserType = user.UserType.Type,
                UserStatus = user.UserStatus.Status,
                Address = addresObj
            };

            return bUserDTO;
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

        public async Task<Response<GetPrivateUserDTO>> CreateNewPrivateUserAsync(PrivateUserDTO pUserDTO) {
            var userType = await _context.UserTypes.Where(t => t.Type == pUserDTO.UserType).FirstOrDefaultAsync();
            var userStatus = await _context.UserStatuses.Where(s => s.Status == pUserDTO.UserStatus).FirstOrDefaultAsync();
            var address = await _context.Addresses.Where(a => a.Address1 == pUserDTO.Address.Address).FirstOrDefaultAsync();
            var business = await _context.BusinessUsers.Where(b => b.Name == pUserDTO.WorkName).FirstOrDefaultAsync();
            var userNameExist = await _context.Users.Where(x => x.Username == pUserDTO.Username).FirstOrDefaultAsync();
            var userPrincipalEmailExist = await _context.Users.Where(x => x.PrincipalEmail == pUserDTO.PrincipalEmail).FirstOrDefaultAsync();
            

            if (userNameExist != null){
                return new Response<GetPrivateUserDTO>("Username exist", ResponseCodeEnum.FAILED);
            } 
            
            if (userPrincipalEmailExist != null) { 
                return new Response<GetPrivateUserDTO>("Email exist", ResponseCodeEnum.FAILED);
            }

            var user = new User
            {
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
            var getPrivateUser = await this.GetPrivateUserDTOAsync(pUser.UserId);
            return new Response<GetPrivateUserDTO>("User registered successfully!", ResponseCodeEnum.SUCCESSED, getPrivateUser);
        }

        public async Task<Response<GetBusinessUserDTO>> CreateNewBusinessUserAsync(BusinessUserDTO bUserDTO) {
            var userType = await _context.UserTypes.Where(t => t.Type == bUserDTO.UserType).FirstOrDefaultAsync();
            var userStatus = await _context.UserStatuses.Where(s => s.Status == bUserDTO.UserStatus).FirstOrDefaultAsync();
            var address = await _context.Addresses.Where(a => a.Address1 == bUserDTO.Address.Address).FirstOrDefaultAsync();
            var userNameExist = await _context.Users.Where(x => x.Username == bUserDTO.Username).FirstOrDefaultAsync();
            var userPrincipalEmailExist = await _context.Users.Where(x => x.PrincipalEmail == bUserDTO.PrincipalEmail).FirstOrDefaultAsync();

            if (userNameExist != null)
            {
                return new Response<GetBusinessUserDTO>("Username exist", ResponseCodeEnum.FAILED);
            }

            if (userPrincipalEmailExist != null)
            {
                return new Response<GetBusinessUserDTO>("Email exist", ResponseCodeEnum.FAILED);
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Image = null,
                Username = bUserDTO.Username,
                Nickname = bUserDTO.Nickname,
                PrincipalEmail = bUserDTO.PrincipalEmail,
                Password = bUserDTO.Password,
                UserTypeId = userType.Id,
                UserStatusId = userStatus.Id,
                AddressId = address.Id,
                LastUpade = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            user = await this.CreateNewUserAsync(user);

            var bUser = new BusinessUser {
                UserId = user.Id,
                Name = bUserDTO.Name,
                AnniversaryDate = DateTime.Parse(bUserDTO.AnniversaryDate)
            };

            await _context.BusinessUsers.AddAsync(bUser);
            await _context.SaveChangesAsync();
            var getBusinessUser = await this.GetBusinessUserDTOAsync(bUser.UserId);
            return new Response<GetBusinessUserDTO>("User registered successfully!", ResponseCodeEnum.SUCCESSED, getBusinessUser);
        }
    }
}