//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Movie.Business.Abstract;
//using Movie.Core.Common.Enums;
//using Movie.Core.Utilities.Cache;
//using Movie.Entities.Concrete;
//using Movie.Entities.Dto.Auth;

//namespace Movie.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [EnableCors("CorsPolicy")]
//    public class AuthController : ControllerBase
//    {
//        #region Constructor
//        private readonly IUserService _userService;
//        public AuthController(IUserService userService)
//        {
//            _userService = userService;
//        }
//        #endregion
//        #region Web
//        [HttpPost("Register")]
//        public IActionResult Register(RegisterDto registerDto)
//        {
//            var response = _userService.Register(registerDto);

//            if (!response.IsSuccess)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//        [HttpPost("Login")]
//        public IActionResult Login(LoginDto loginDto)
//        {
//            var response = _userService.Login(loginDto);

//            if (!response.IsSuccess)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//        [HttpPost("ConfirmAccount")]
//        public IActionResult ConfirmAccount(ConfirmAccountDto confirmAccountDto)
//        {
//            var response = _userService.ConfirmAccount(confirmAccountDto);

//            if (!response.IsSuccess)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//        [HttpPost("ForgetPassword")]
//        public IActionResult ForgetPassword(ForgetPasswordDto forgetPasswordDto)
//        {
//            var response = _userService.ForgetPassword(forgetPasswordDto);

//            if (!response.IsSuccess)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//        [HttpGet("ConfirmPasswordChange")]
//        public IActionResult ConfirmPasswordChange(ConfirmPasswordChangeDto confirmPasswordChangeDto)
//        {
//            var response = _userService.ConfirmPasswordChange(confirmPasswordChangeDto);

//            if (!response.IsSuccess)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);

//        }

//        [HttpPost("PasswordChange")]
//        public IActionResult PasswordChange(PasswordChangeDto passwordChangeDto)
//        {
//            var response = _userService.PasswordChange(passwordChangeDto);

//            if (!response.IsSuccess)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }

//        #endregion
//        #region Admin
//        #endregion
//    }
//}