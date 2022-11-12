using WebApiCalls.Data;
using WebApiCalls.Models;
using WebApiCalls.Models.Requests;
using WebApiCalls.Models.Validators;
using WebApiCalls.Services;
using WebApiCalls.Services.Impl;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System;

namespace WebApiCalls.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {

        #region Services

        private readonly ICallsRepository _callRepository;
        private readonly IContactsRepository _contactRepository;
        private readonly IValidator<CallDto> _callDtoVailadtor;
        private readonly IValidator<CreateCallRequest> _createCallRequestDtoVailadtor;

        #endregion

        #region Constructors

        public CallController(
            ICallsRepository callRepository,
            IContactsRepository contactRepository,
            IValidator<CallDto> callDtoVailadtor,
            IValidator<CreateCallRequest> createCallRequestDtoVailadtor
            )
        {
            _callRepository = callRepository;
            _contactRepository = contactRepository;
            _callDtoVailadtor = callDtoVailadtor;
            _createCallRequestDtoVailadtor = createCallRequestDtoVailadtor;
        }

        #endregion

        #region Public Methods

        [HttpPost("create")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CallDto), StatusCodes.Status200OK)]
        public ActionResult<int> Create([FromBody] CreateCallRequest request)
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ');
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var res = tokenHandler.ReadToken(jwt[1]);
            string contactIdString = res.ToString()
                .Replace('.', ' ')
                .Replace(',', ' ')
                .Replace('}', ' ')
                .Replace('{', ' ')
                .Replace('"', ' ')
                .Replace(':', ' ')
                .Replace("   ", " ")
                .Replace("  ", " ")
                .Split("ContactId ")[1]
                .Split(' ')[0];
            int fromId = int.Parse(contactIdString);
            


            ValidationResult validationResult = _createCallRequestDtoVailadtor.Validate(request);

            int toId = _contactRepository.FindIdPhone(request.ToPhone);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            if (fromId == toId)
                return BadRequest("Doesn't call himself");

            _callRepository.Create(new Call
            {
               FromId = fromId,
               ToId = toId,
               TimeStart = request.TimeStart,
               TimeEnd = request.TimeEnd
            });
            return Ok();
           
        }

        [HttpGet("all")]
        public ActionResult<IQueryable<CallDto>> GetAll()
        {
            return Ok(_callRepository.GetAll().Select(call => new CallDto
            {
                CallId = call.CallId,
                FromPhone = _contactRepository.GetById(call.FromId).Phone,
                ToPhone = _contactRepository.GetById(call.ToId).Phone,
                TimeStart = call.TimeStart,
                TimeEnd = call.TimeEnd
            }).ToList());
        }

        [HttpGet("get/{id}")]
        public ActionResult<CallDto> GetById([FromRoute] int id)
        {
            var call = _callRepository.GetById(id);
            return Ok(new CallDto
            {
                CallId = call.CallId,
                FromPhone = _contactRepository.GetById(call.FromId).Phone,
                ToPhone = _contactRepository.GetById(call.ToId).Phone,
                TimeStart = call.TimeStart,
                TimeEnd = call.TimeEnd
            });
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CallDto), StatusCodes.Status200OK)]
        public ActionResult<CallDto> Update([FromBody] CallDto callDto)
        {
            ValidationResult validationResult = _callDtoVailadtor.Validate(callDto);
            int fromId = _contactRepository.FindIdPhone(callDto.FromPhone);
            int toId = _contactRepository.FindIdPhone(callDto.ToPhone);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            if (fromId == toId)
                return BadRequest("FromPhone not equal ToPhone");

            _callRepository.Update(new Call
            {
                CallId = callDto.CallId,
                FromId = fromId,
                ToId = toId,
                TimeStart = callDto.TimeStart,
                TimeEnd = callDto.TimeEnd
            });
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<CallDto> Delete([FromRoute] int id)
        {
            _callRepository.Delete(id);
            return Ok();
        }

        #endregion

       

    }

}
