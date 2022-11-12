using WebApiCalls.Data;
using WebApiCalls.Models;
using WebApiCalls.Models.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApiCalls.Services.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        public const string SecretKey = "kYp3s6v9y/B?E(H+";

        private readonly Dictionary<string, SessionDto> _sessions =
            new Dictionary<string, SessionDto>();

        #region Services

        private readonly IServiceScopeFactory _serviceScopeFactory;

        #endregion


        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public SessionDto GetSession(string sessionToken)
        {
            SessionDto sessionDto;

            lock (_sessions)
            {
               _sessions.TryGetValue(sessionToken, out sessionDto);
            }

            if (sessionDto == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                WebApiCallsDbContext context = scope.ServiceProvider.GetRequiredService<WebApiCallsDbContext>();

                ContactSession session = context
                    .ContactSessions
                    .Where(x => x.SessionToken == sessionToken)
                    .FirstOrDefault();
                if (sessionDto == null)
                    return null;

                Contact contact = context.Contacts.Find(session.ContactId);

                sessionDto = GetSessionDto(contact, session);
                if(sessionDto != null)
                {
                    lock (_sessions)
                    {
                        _sessions[sessionToken] = sessionDto;
                    }
                }
            }

            return sessionDto;
        }


        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            WebApiCallsDbContext context = scope.ServiceProvider.GetRequiredService<WebApiCallsDbContext>();

            Contact contact =
               !string.IsNullOrWhiteSpace(authenticationRequest.Phone) ?
               FindAccountByLogin(context, authenticationRequest.Phone) : null;

            if (contact == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.PhoneNotFound
                };
            }

            ContactSession session = new ContactSession
            {
                ContactId = contact.ContactId,
                SessionToken = CreateSessionToken(contact),
                TimeCreated = DateTime.Now,
                TimeLastRequest = DateTime.Now,
                IsClosed = false,
            };

            context.ContactSessions.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(contact, session);

            lock (_sessions)
            {
                _sessions[session.SessionToken] = sessionDto;
            }

            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                Session = sessionDto
            };
        }


        private SessionDto GetSessionDto(Contact contact, ContactSession contactSession)
        {


            return new SessionDto
            {
                SessionId = contactSession.SessionId,
                SessionToken = contactSession.SessionToken,
                ContactId = contact.ContactId
            };
        }


        private string CreateSessionToken(Contact contact)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]{
                        new Claim("ContactId", contact.ContactId.ToString()),
                        new Claim(ClaimTypes.MobilePhone, contact.Phone),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Contact FindAccountByLogin(WebApiCallsDbContext context, string login)
        {
            return context
                .Contacts
                .Where(x => x.Phone == login)
                .FirstOrDefault();
        }
    }
}
