using kontacto_api.DTO;
using kontacto_api.Services;
using kontacto_api.Tools;
using kontacto_api.Tools.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kontacto_api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.GetUserAsync(id);
            
            if (user == null) {
                return NotFound();
            }
            
            return Ok(new Response<object>("User founded!", ResponseCodeEnum.SUCCESS, user));
        }

        [HttpPost("private")]
        public async Task<Response<GetPrivateUserDTO>> RegisterNewUser(PrivateUserDTO user) {
            return await _service.CreateNewPrivateUserAsync(user);
        }

        [HttpPost("business")]
        public async Task<Response<GetBusinessUserDTO>> RegisterNewUser(BusinessUserDTO user) {
            return await _service.CreateNewBusinessUserAsync(user);
        }
    }
}
