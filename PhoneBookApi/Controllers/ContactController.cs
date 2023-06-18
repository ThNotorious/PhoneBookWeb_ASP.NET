using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookWeb_Api.Interfaces;
using PhoneBookWeb_Api.Models;
using System.Data;

namespace PhoneBookWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("GetContacts")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        [Route("GetOneContactById/{id}")]
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetOneContactById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [Route("ContactAdd")]
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ContactAdd(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOneContactById), new { id = contact.ID }, contact);
        }

        [Route("EditContact")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditContact([FromBody] Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Route("DeleteContact/{id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
