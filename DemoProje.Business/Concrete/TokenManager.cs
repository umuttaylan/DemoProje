using DemoProje.Business.Abstract;
using DemoProje.Business.CustomExtensions;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class TokenManager : ITokenSerivce
    {
        readonly AppSettings _appSettings;

        public TokenManager(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        List<Identity> _users = new List<Identity>{
            new Identity{Id=1,FirstName="Test",LastName="User",UserName="test",Password="test"}
        };
        public ResponseViewModel Authenticate(AuthenticateDto authenticateDto)
        {
            var response = new ResponseViewModel();
            var user = _users.SingleOrDefault(x => x.UserName == authenticateDto.UserName&& x.Password == authenticateDto.Password);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı bulunamadı";

                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;

            response.Data = user;

            return response;
        }
    }
}
