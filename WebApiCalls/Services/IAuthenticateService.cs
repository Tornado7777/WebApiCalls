using WebApiCalls.Models;
using WebApiCalls.Models.Requests;

namespace WebApiCalls.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionDto GetSession(string sessionToken);
    }
}
