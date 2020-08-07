//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Movie.Business.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Movie.Business.CustomExtensions
//{
//    public class TokenAttribute : ActionFilterAttribute
//    {
//        private readonly ITokenService _tokenService;
//        public TokenAttribute(ITokenService tokenService)
//        {
//            _tokenService = tokenService;
//        }
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            var token = context.HttpContext.Request.Headers["authorization"].ToString();
//            if (!string.IsNullOrEmpty(token))
//            {
//                if (!_tokenService.ValidToken(token))
//                    context.Result = new UnauthorizedResult();
//            }
//            else
//                context.Result = new UnauthorizedResult();
//        }
//    }
//}
