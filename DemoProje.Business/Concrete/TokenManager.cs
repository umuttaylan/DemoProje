//using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using Movie.Business.Abstract;
//using Movie.Core.Common.Enums;
//using Movie.Core.Entities;
//using Movie.Core.Utilities.Cache;
//using Movie.Entities.Concrete;
//using Movie.Entities.Dto.User;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;

//namespace Movie.Business.Concrete
//{
//    public class TokenManager : ITokenService
//    {
//        #region Constructor
//        private readonly IConfiguration _configuration;
//        private readonly IDistributedCache _cache;
//        private readonly MovieOptions _options;
//        public TokenManager(IConfiguration configuration,
//                            IDistributedCache cacheService,
//                            IOptions<MovieOptions> options
//                            )
//        {
//            _configuration = configuration;
//            _cache = cacheService;
//            _options = options.Value;
//        }
//        #endregion
//        #region Method
//        public UserTokenDto CreateToken(User user)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();

//            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Email, user.EmailHash.ToString()),
//                    new Claim(ClaimTypes.Expired,DateTime.Now.ToLongDateString())

//                }),
//                Expires = DateTime.Now.AddDays(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
//                    , SecurityAlgorithms.HmacSha512Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var userToken = new UserTokenDto()
//            {
//                ExpireDate = DateTime.Now,
//                Token = tokenHandler.WriteToken(token)
//            };
//            return userToken;
//        }
//        public User TokenValidation(string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
//                var parameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ClockSkew = TimeSpan.FromDays(1)
//                };
//                SecurityToken validatedToken;
//                var tokenParameters = TokenValidationParameters.DefaultAuthenticationType;
//                var user = tokenHandler.ValidateToken(token, parameters, out validatedToken);
//                if (user.Identities.Count() > 0)
//                {
//                    return null; //_userService.GetUserByEmail(user.Identities.First().Name);
//                }
//                else
//                {
//                    User newUser = new User();
//                    return newUser;
//                }
//            }
//            catch (Exception)
//            {
//                User newUser = new User();
//                return newUser;
//            }
//        }
//        public bool ValidToken(string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
//                var parameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ClockSkew = TimeSpan.FromDays(1)
//                };
//                SecurityToken validatedToken;
//                var tokenParameters = TokenValidationParameters.DefaultAuthenticationType;
//                var user = tokenHandler.ValidateToken(token, parameters, out validatedToken);
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }
//        #endregion
//    }
//}
