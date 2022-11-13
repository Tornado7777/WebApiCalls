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
using System.Linq;
using System.Collections.Generic;

namespace WebApiCalls.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        #region Services

        private readonly IContactsRepository _contactRepository;
        private readonly IValidator<ContactDto> _contactDtoVailadtor;

        #endregion

        #region Constructors

        public ContactController(
            IContactsRepository contactRepository,
            IValidator<ContactDto> contactDtoVailadtor)
        {
            _contactRepository = contactRepository;

            _contactDtoVailadtor = contactDtoVailadtor;
        }

        #endregion

        #region Public Methods

        [HttpPut("create")]
        public ActionResult<int> Create([FromBody] CreateContactRequest request)
        {
            return Ok(_contactRepository.Create(new Contact
            {
                Phone = request.Phone,
                FIO = request.FIO,
                Company = request.Company,
                Description = request.Description,                
            }
            ));       
        }

        [HttpGet("all")]
        public ActionResult<IQueryable<ContactDto>> GetAllDepartments()
        {
            return Ok(_contactRepository.GetAll().Select(contact => new ContactDto
            {
                ContactId = contact.ContactId,
                Phone = contact.Phone,
                FIO = contact.FIO,
                Company = contact.Company,
                Description = contact.Description
            }).ToList());
        }

        [HttpGet("get/{id}")]
        public ActionResult<ContactDto> GetById([FromRoute] int id)
        {
            var contact = _contactRepository.GetById(id);
            return Ok(new ContactDto
            {
                ContactId = contact.ContactId,
                Phone = contact.Phone,
                FIO = contact.FIO,
                Company = contact.Company,
                Description = contact.Description
            });
        }

        [HttpPost ("update")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
        public ActionResult<ContactDto> Update([FromBody] ContactDto contactDto)
        {
            ValidationResult validationResult = _contactDtoVailadtor.Validate(contactDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            _contactRepository.Update(new Contact
            {
                ContactId = contactDto.ContactId,
                Phone = contactDto.Phone,
                FIO = contactDto.FIO,
                Company = contactDto.Company,
                Description = contactDto.Description
            });
            return Ok();
        }

        [HttpDelete ("delete/{id}")]
        public ActionResult<ContactDto> Delete([FromRoute] int id)
        {
            _contactRepository.Delete(id);
            return Ok();
        }

        #endregion

    }
}
