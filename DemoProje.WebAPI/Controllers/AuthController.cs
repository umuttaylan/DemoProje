using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProje.Business.Abstract;
using DemoProje.Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProje.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto authenticate)
        {

            var user = await _authService.Authenticate(authenticate.UserName, authenticate.Password);

            if (user == null)
                return BadRequest(new { message = "UserName or Passoword is incorrect!" });

            return Ok(user);

        }
    }
}
