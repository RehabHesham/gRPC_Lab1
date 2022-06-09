using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace StoreAPI_client_grpc_
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                var value = Request.Headers["Authorization"].ToString();
                if (value.StartsWith("Basic"))
                {
                    var token = value.Split(" ")[1];
                    var bytes = Convert.FromBase64String(token);
                    var plainText = Encoding.UTF8.GetString(bytes);
                    int separatorIndex = plainText.IndexOf(":");
                    string username = plainText.Substring(0,separatorIndex);
                    string password = plainText.Substring(separatorIndex + 1);

                    if(username == "device" && password == "P@ssw0rd")
                    {
                        var principal = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                new List<Claim> { 
                                    new Claim(ClaimTypes.NameIdentifier, username),
                                    new Claim(ClaimTypes.Role, "device")
                        }));
                        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal , Scheme.Name)));
                    }
                }
            }
            return Task.FromResult(AuthenticateResult.Fail("UnAuthenticated"));
        }
    }
}
