using kontacto_api.DTO;
using kontacto_api.Services;
using kontacto_api.Tools;
using kontacto_api.Tools.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kontacto_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.GetUserAsync(id);
            
            if (user == null) {
                return NotFound(new Response<string>("User not found!", ResponseCodeEnum.FAILED));
            }
            
            return Ok(new Response<object>("", ResponseCodeEnum.SUCCESSED, user));
        }

        [HttpPost("private")]
        public async Task<IActionResult> RegisterNewUser(PrivateUserDTO user) {
            var response = await _service.CreateNewPrivateUserAsync(user);
            return Ok(response);
        }

        [HttpPost("business")]
        public async Task<IActionResult> RegisterNewUser(BusinessUserDTO user) {
            var response = await _service.CreateNewBusinessUserAsync(user);
            return Ok(response);
        }

    }
}
