using DemoProje.Business.Abstract;
using DemoProje.Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DemoProje.Business.Concrete.Helpers
{
    public class BasicAuthenticationHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IAuthService _authService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
                                          ILoggerFactory logger, 
                                          UrlEncoder encoder, 
                                          ISystemClock clock, 
                                          IAuthService authService) : base(options, logger, encoder, clock)
        {
            _authService= authService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            Identity identity = null;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var userName = credentials[0];
                var password = credentials[1];

                identity = await _authService.Authenticate(userName, password);

            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (identity == null)
                return AuthenticateResult.Fail("Invalid UserName or Password");

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,identity.Id.ToString()),
                new Claim(ClaimTypes.Name,identity.UserName)
            };

            var identity2 = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity2);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
