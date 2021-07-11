using System;
using System.Linq;
using System.Threading.Tasks;
using kontacto_api.Data;
using kontacto_api.DTO;
using kontacto_api.Models;
using kontacto_api.Tools;
using kontacto_api.Tools.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static kontacto_api.Tools.Encryptor;

namespace kontacto_api.Services
{
    public class UserService
    {
        private readonly KontactoContext _context;
        public UserService(KontactoContext context)
        {
            _context = context;
        }
        private async Task<GetPrivateUserDTO> GetPrivateUserDTOAsync(string id)
        {
            var user = await _context.Users
                .Include(t => t.UserType)
                .Include(a => a.Address)
                .Include(b => b.BusinessUser)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return (null);

            var city = await _context.AddressCities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(ct => ct.Id == user.Address.CityId);

            var pUser = await _context.PrivateUsers.FindAsync(id);

            var addresObj = new AddressDTO
            {
                Address = user.Address.Address1,
                SecondAddress = user.Address.SecondAddress,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
                City = city.Name,
                Country = city.Country.Name,
            };

            var pUserDTO = new GetPrivateUserDTO
            {
                Id = user.Id,
                FirstName = pUser.FirstName,
                SecondName = pUser.SecondName,
                FirstSurname = pUser.FirstSurname,
                SecondSurname = pUser.SecondSurname,
                WorkName = user.BusinessUser?.Name,
                IsWorking = pUser.IsWorking,
                Occupation = pUser.Occupation,
                BirthDate = pUser.BirthDate.ToString("yyyy/MM/dd"),
                Image = user.Image,
                Username = user.Username,
                Nickname = pUser.Nickname,
                PrincipalEmail = user.PrincipalEmail,
                UserType = user.UserType.Type,
                Address = addresObj
            };

            return pUserDTO;
        }
        private async Task<GetBusinessUserDTO> GetBusinessUserDTOAsync(string id)
        {
            var user = await _context.Users
                .Include(t => t.UserType)
                .Include(a => a.Address)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return (null);

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
            };

            var bUserDTO = new GetBusinessUserDTO
            {
                Id = user.Id,
                Name = bUser.Name,
                AnniversaryDate = bUser.AnniversaryDate.ToString("yyyy/MM/dd"),
                Image = user.Image,
                Username = user.Username,
                PrincipalEmail = user.PrincipalEmail,
                UserType = user.UserType.Type,
                Address = addresObj
            };

            return bUserDTO;
        }
        public async Task<object> GetUserAsync(string id)
        {
            var userType = await this.GetUserTypeAsync(id);
            return userType != null ? (
                userType == "PRIVATE" ? await this.GetPrivateUserDTOAsync(id) :
                await this.GetBusinessUserDTOAsync(id)) : null;
        }
        private async Task<string> GetUserTypeAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            var userType = await _context.UserTypes.FindAsync(user.UserTypeId);
            return userType.Type;
        }
        private async Task<User> CreateNewUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<Response<GetPrivateUserDTO>> CreateNewPrivateUserAsync(PrivateUserDTO pUserDTO)
        {
            var userType = await _context.UserTypes.Where(t => t.Type == pUserDTO.UserType).FirstOrDefaultAsync();
            var userStatus = await _context.UserStatuses.Where(s => s.Status == "ACTIVE").FirstOrDefaultAsync();
            var address = await _context.Addresses.Where(a => a.Address1 == pUserDTO.Address.Address).FirstOrDefaultAsync();
            var business = await _context.BusinessUsers.Where(b => b.Name == pUserDTO.WorkName).FirstOrDefaultAsync();
            var userNameExist = await _context.Users.Where(x => x.Username == pUserDTO.Username).FirstOrDefaultAsync();
            var userPrincipalEmailExist = await _context.Users.Where(x => x.PrincipalEmail == pUserDTO.PrincipalEmail).FirstOrDefaultAsync();
            var emailRegex = "[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}";
            var onlyLettersRegex = "\\p{L}";
            var email = pUserDTO.PrincipalEmail;
            var actualDate = DateTime.Now;
            var date = DateTime.Parse(pUserDTO.BirthDate);
            var maxDaysDifference = 3650;
            var daysDifference = actualDate.Subtract(date).TotalDays;

            var hasNullData = (
                pUserDTO.FirstName == null ||
                pUserDTO.FirstName == "" ||
                pUserDTO.FirstName == " " ||
                pUserDTO.FirstSurname == null ||
                pUserDTO.FirstSurname == "" ||
                pUserDTO.FirstSurname == " " ||
                pUserDTO.BirthDate == null ||
                pUserDTO.BirthDate == "" ||
                pUserDTO.BirthDate == " " ||
                pUserDTO.Password == null ||
                pUserDTO.Password == "" ||
                pUserDTO.Password == " " ||
                pUserDTO.PrincipalEmail == null ||
                pUserDTO.PrincipalEmail == "" ||
                pUserDTO.PrincipalEmail == " " ||
                pUserDTO.Username == null ||
                pUserDTO.Username == "" ||
                pUserDTO.Username == " " ||
                pUserDTO.UserType == null ||
                pUserDTO.UserType == "" ||
                pUserDTO.UserType == " " ||
                pUserDTO.IsWorking == null ||
                pUserDTO.Address == null
            );

            var hasNotOnlyLetters = (
                !Regex.IsMatch(pUserDTO.FirstName ?? "a", onlyLettersRegex) ||
                Regex.Replace(pUserDTO.FirstName ?? "a", onlyLettersRegex, string.Empty).Length != 0 ||
                !Regex.IsMatch(pUserDTO.SecondName ?? "a", onlyLettersRegex) ||
                Regex.Replace(pUserDTO.SecondName ?? "a", onlyLettersRegex, string.Empty).Length != 0 ||
                !Regex.IsMatch(pUserDTO.FirstSurname ?? "a", onlyLettersRegex) ||
                Regex.Replace(pUserDTO.FirstSurname ?? "a", onlyLettersRegex, string.Empty).Length != 0 ||
                !Regex.IsMatch(pUserDTO.SecondSurname ?? "a", onlyLettersRegex) ||
                Regex.Replace(pUserDTO.SecondSurname ?? "a", onlyLettersRegex, string.Empty).Length != 0
            );

            if (userNameExist != null)
            {
                return new Response<GetPrivateUserDTO>("Username exist", ResponseCode.FAILED);
            }

            if (userPrincipalEmailExist != null)
            {
                return new Response<GetPrivateUserDTO>("Email exist", ResponseCode.FAILED);
            }

            if (!Regex.IsMatch(email, emailRegex) || Regex.Replace(email, emailRegex, string.Empty).Length != 0)
            {
                return new Response<GetPrivateUserDTO>("Wrong email format", ResponseCode.FAILED);
            }

            if (daysDifference <= maxDaysDifference)
            {
                return new Response<GetPrivateUserDTO>("The date must be greater than 10 years", ResponseCode.FAILED);
            }

            if (hasNullData)
            {
                return new Response<GetPrivateUserDTO>("Has required fields empty", ResponseCode.FAILED);
            }

            if (hasNotOnlyLetters)
            {
                return new Response<GetPrivateUserDTO>("Name fields only accept letters", ResponseCode.FAILED);
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Image = pUserDTO.Image ?? null,
                Username = pUserDTO.Username,
                PrincipalEmail = pUserDTO.PrincipalEmail,
                Password = Encrypt(pUserDTO.Password, 2),
                UserTypeId = userType.Id,
                UserStatusId = userStatus.Id,
                AddressId = address.Id,
                CreatedAt = DateTime.UtcNow
            };

            var userIdExist = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            user.Id = userIdExist == null ? user.Id : Guid.NewGuid().ToString();

            user = await this.CreateNewUserAsync(user);

            var pUser = new PrivateUser
            {
                UserId = user.Id,
                FirstName = pUserDTO.FirstName,
                Nickname = pUserDTO.Nickname == "" ? null : pUserDTO.Nickname,
                SecondName = pUserDTO.SecondName == "" ? null : pUserDTO.SecondName,
                FirstSurname = pUserDTO.FirstSurname,
                SecondSurname = pUserDTO.SecondSurname == "" ? null : pUserDTO.SecondSurname,
                BusinessId = business == null || business.UserId == "" ? null : business.UserId,
                IsWorking = pUserDTO.IsWorking ?? false,
                Occupation = pUserDTO.Occupation == "" ? null : pUserDTO.Occupation,
                BirthDate = DateTime.Parse(pUserDTO.BirthDate)
            };

            await _context.PrivateUsers.AddAsync(pUser);
            await _context.SaveChangesAsync();

            var getPrivateUser = await this.GetPrivateUserDTOAsync(pUser.UserId);

            return new Response<GetPrivateUserDTO>("User registered successfully!", ResponseCode.SUCCESS, getPrivateUser);
        }
        public async Task<Response<GetBusinessUserDTO>> CreateNewBusinessUserAsync(BusinessUserDTO bUserDTO)
        {
            var userType = await _context.UserTypes.Where(t => t.Type == bUserDTO.UserType).FirstOrDefaultAsync();
            var userStatus = await _context.UserStatuses.Where(s => s.Status == "ACTIVE").FirstOrDefaultAsync();
            var address = await _context.Addresses.Where(a => a.Address1 == bUserDTO.Address.Address).FirstOrDefaultAsync();
            var userNameExist = await _context.Users.Where(x => x.Username == bUserDTO.Username).FirstOrDefaultAsync();
            var userPrincipalEmailExist = await _context.Users.Where(x => x.PrincipalEmail == bUserDTO.PrincipalEmail).FirstOrDefaultAsync();
            var emailRegex = "[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}";
            var email = bUserDTO.PrincipalEmail;
            var actualDate = DateTime.Now;
            var date = DateTime.Parse(bUserDTO.AnniversaryDate);

            var hasNullData = (
                (bUserDTO.Name == null || bUserDTO.Name == "") ||
                (bUserDTO.Password == null || bUserDTO.Password == "") ||
                (bUserDTO.PrincipalEmail == null || bUserDTO.PrincipalEmail == "") ||
                (bUserDTO.Username == null || bUserDTO.Username == "") ||
                (bUserDTO.UserType == null || bUserDTO.UserType == "") ||
                (bUserDTO.Address == null)
            );

            if (userNameExist != null)
            {
                return new Response<GetBusinessUserDTO>("Username exist", ResponseCode.FAILED);
            }

            if (userPrincipalEmailExist != null)
            {
                return new Response<GetBusinessUserDTO>("Email exist", ResponseCode.FAILED);
            }

            if (!Regex.IsMatch(email, emailRegex) || Regex.Replace(email, emailRegex, string.Empty).Length != 0)
            {
                return new Response<GetBusinessUserDTO>("Wrong email format", ResponseCode.FAILED);
            }

            if (date >= actualDate)
            {
                return new Response<GetBusinessUserDTO>("The date entered is greater than the current one", ResponseCode.FAILED);
            }

            if (hasNullData)
            {
                return new Response<GetBusinessUserDTO>("Has required fields empty", ResponseCode.FAILED);
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Image = null,
                Username = bUserDTO.Username,
                PrincipalEmail = bUserDTO.PrincipalEmail,
                Password = Encrypt(bUserDTO.Password, 2),
                UserTypeId = userType.Id,
                UserStatusId = userStatus.Id,
                AddressId = address.Id,
                CreatedAt = DateTime.UtcNow
            };

            user = await this.CreateNewUserAsync(user);

            var bUser = new BusinessUser
            {
                UserId = user.Id,
                Name = bUserDTO.Name,
                AnniversaryDate = DateTime.Parse(bUserDTO.AnniversaryDate)
            };

            await _context.BusinessUsers.AddAsync(bUser);
            await _context.SaveChangesAsync();
            var getBusinessUser = await this.GetBusinessUserDTOAsync(bUser.UserId);
            return new Response<GetBusinessUserDTO>("User registered successfully!", ResponseCode.SUCCESS, getBusinessUser);
        }
    }
}