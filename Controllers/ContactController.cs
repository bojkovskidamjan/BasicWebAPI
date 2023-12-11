using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicWebAPI.Data;
using BasicWebAPI.Model;
using BasicWebAPI.Model.DTO;
using BasicWebAPI.Services.Implementation;
using BasicWebAPI.Services.Interfaces;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DisplayContactsDTO>>> GetAllContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContactsAsync();

                var contactDTOs = contacts.Select(contact => new DisplayContactsDTO
                {
                    ContactName = contact.ContactName,
                    CompanyId = contact.CompanyId,
                    CountryId = contact.CountryId,
                    CompanyName = contact.Company.CompanyName,
                    CountryName = contact.Country.CountryName
                }).ToList();

                return Ok(contactDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching all contacts: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDTO createContactDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contact = new Contact
                    {
                        ContactName = createContactDto.ContactName,
                        CompanyId = createContactDto.CompanyId,
                        CountryId = createContactDto.CountryId
                    };

                    await _contactService.CreateContactAsync(contact);
                    return Ok(contact);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error occurred while creating the contact: " + ex.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(contactId);

                if (contact == null)
                {
                    return NotFound();
                }

                _contactService.DeleteContact(contact);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while deleting the contact: " + ex.Message);
            }
        }



        [HttpPut("{contactId}")]
        public async Task<IActionResult> UpdateContact(int contactId, [FromBody] ContactDTO updateContactDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingContact = await _contactService.GetContactByIdAsync(contactId);

                    if (existingContact == null)
                    {
                        return NotFound();
                    }

                    existingContact.ContactName = updateContactDto.ContactName;
                    existingContact.CompanyId = updateContactDto.CompanyId;
                    existingContact.CountryId = updateContactDto.CountryId;

                    _contactService.UpdateContact(existingContact);

                    return Ok(existingContact);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error occurred while updating the contact: " + ex.Message);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpGet("FilterContact")]
        public async Task<ActionResult<List<DisplayContactsDTO>>> FilterContacts(int? countryId, int? companyId)
        {
            try
            {
                var filteredContacts = await _contactService.FilterContactsAsync(countryId, companyId);

                var contactDTOs = filteredContacts.Select(contact => new DisplayContactsDTO
                {
                    ContactName = contact.ContactName,
                    CompanyId = contact.CompanyId,
                    CountryId = contact.CountryId,
                    CompanyName = contact.Company.CompanyName,
                    CountryName = contact.Country.CountryName
                }).ToList();

                return Ok(contactDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while filtering contacts: " + ex.Message);
            }
        }
    }
}