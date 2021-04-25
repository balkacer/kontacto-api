using System;
using System.Threading.Tasks;
using kontacto_api.Data;
using kontacto_api.DTO;
using kontacto_api.Models;

namespace kontacto_api.Services
{
    public class AuthService
    {
        private readonly KontactoContext _context;
        public AuthService(KontactoContext context)
        {
            _context = context;
        }

        public async Task<GetPrivateUserDTO> GetPrivateUserDTOAsync(string id) {
            var user = await _context.Users.FindAsync(id);
            var pUser = await _context.PrivateUsers.FindAsync(id);

            var pUserDTO = new GetPrivateUserDTO {
                UserId = user.Id,
                FirstName = pUser.FirstName,
                SecondName = pUser.SecondName,
                FirstSurname = pUser.FirstSurname,
                SecondSurname = pUser.SecondSurname,
                BusinessId = pUser.BusinessId,
                IsWorking = pUser.IsWorking,
                Ocupation = pUser.Ocupation,
                BirthDate = pUser.BirthDate.ToString("dd/MM/yyyy"),
                Image = user.Image,
                Username = user.Username,
                Nickname = user.Nickname,
                PrincipalEmail = user.PrincipalEmail,
                UserTypeId = user.UserTypeId,
                UserStatusId = user.UserStatusId,
                AddressId = user.AddressId
            };

            return pUserDTO;
        }

        private async Task<User> CreateNewUserAsync(User user) {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<PrivateUser> CreateNewPrivateUserAsync(PrivateUserDTO pUserDTO) {
            var user = new User {
                Id = Guid.NewGuid().ToString(),
                Image = pUserDTO.Image,
                Username = pUserDTO.Username,
                Nickname = pUserDTO.Nickname,
                PrincipalEmail = pUserDTO.PrincipalEmail,
                Password = pUserDTO.Password,
                UserTypeId = pUserDTO.UserTypeId,
                UserStatusId = pUserDTO.UserStatusId,
                AddressId = pUserDTO.AddressId,
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
                BusinessId = pUserDTO.BusinessId,
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