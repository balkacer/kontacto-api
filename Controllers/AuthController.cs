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
                return NotFound(new Response("User not found!", ResponseCodeEnum.FAILED));
            }
            
            return Ok(new Response("", ResponseCodeEnum.SUCCESSED, user));
        }

        [HttpPost("private")]
        public async Task<IActionResult> RegisterNewPrivateUser(PrivateUserDTO userDTO) {
            var pUser = await _service.CreateNewPrivateUserAsync(userDTO);
            var user = await _service.GetUserAsync(pUser.UserId);
            return Ok(new Response("", ResponseCodeEnum.SUCCESSED, user));
        }
    }
}
