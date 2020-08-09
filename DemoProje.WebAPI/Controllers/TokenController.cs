//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DemoProje.Business.Abstract;
//using DemoProje.Entities.Dto;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace DemoProje.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TokenController : ControllerBase
//    {
//        ITokenSerivce _tokenService;

//        public TokenController(ITokenSerivce tokenService)
//        {
//            _tokenService = tokenService;
//        }

//        [AllowAnonymous]
//        [HttpPost("authenticate")]
//        public IActionResult Authenticate([FromBody] AuthenticateDto model)
//        {
//            var response = _tokenService.Authenticate(model);

//            if (!response.IsSuccess)
//                return BadRequest(response);

//            return Ok(response);
//        }
//    }
//}
