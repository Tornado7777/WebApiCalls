using WebApiCalls.Models;
using WebApiCalls.Models.Requests;
using WebApiCalls.Models.Validators;
using WebApiCalls.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace WebApiCalls.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        #region Services

        private readonly IAuthenticateService _authenticateService;
        private readonly IValidator<AuthenticationRequest> _authenticationRequestValidator;

        #endregion


        public AuthenticateController(
            IAuthenticateService authenticateService,
            IValidator<AuthenticationRequest> authenticationRequestValidator)
        {
            _authenticateService = authenticateService;
            _authenticationRequestValidator = authenticationRequestValidator;
        }
        /// <summary>
        /// Authorization by phone number
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "phone": "+1-111-111-11-11"
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            ValidationResult validationResult = _authenticationRequestValidator.Validate(authenticationRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
            if (authenticationResponse.Status == AuthenticationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.Session.SessionToken);
                //Response.Headers.WWWAuthenticate = authenticationResponse.Session.SessionToken;
            }
            return Ok(authenticationResponse);
        }

        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
        public IActionResult GetSession()
        {
           var authorizationHeader =  Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var scheme = headerValue.Scheme; // Bearer
                var sessionToken = headerValue.Parameter; // Token
                //проверка на null или пустой
                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();

                SessionDto sessionDto = _authenticateService.GetSession(sessionToken);
                //если сессия (не) найдена
                if (sessionDto == null)
                    return Unauthorized();

                return Ok(sessionDto);

            }
            return Unauthorized();

        }


    }
}
