using kontacto_api.DTO;
using kontacto_api.Models;
using kontacto_api.Services;
using kontacto_api.Tools;
using kontacto_api.Tools.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("private/{id}")]
        public async Task<IActionResult> GetPrivateUser(string id)
        {
            var pUser = await _service.GetPrivateUserDTOAsync(id);
            
            if (pUser == null) {
                return NotFound();
            }
            
            return Ok(new Response("", ResponseCodeEnum.SUCCESSED, pUser));
        }

        [HttpPost("private")]
        public async Task<IActionResult> RegisterNewPrivateUser(PrivateUserDTO userDTO) {
            var pUser = await _service.CreateNewPrivateUserAsync(userDTO);
            var pUserDTO = await _service.GetPrivateUserDTOAsync(pUser.UserId);
            return Ok(new Response("", ResponseCodeEnum.SUCCESSED, pUserDTO));
        }
    }
}
