using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookWeb_Api.Interfaces;
using PhoneBookWeb_Api.Models;

namespace PhoneBookWeb_Api.Data
{
    public class DataContact : IDataContact
    {
        private readonly ApplicationDbContext _context;

        public DataContact(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task AddContact(Contact persone)
        {
            _context.Contacts.Add(persone);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContact(int personeId)
        {
            var persone = await _context.Contacts.FirstOrDefaultAsync(e => e.ID == personeId);

            _context.Contacts.Remove(persone);
            await _context.SaveChangesAsync();
        }

        public async Task EditContact(Contact contact)
        {
            var contactEdit = await _context.Contacts.FirstOrDefaultAsync(e => e.ID == contact.ID);

            contactEdit.SecondName = contact.SecondName;
            contactEdit.FirstName = contact.FirstName;
            contactEdit.MiddleName = contact.MiddleName;
            contactEdit.PhoneNumber = contact.PhoneNumber;
            contactEdit.Description = contact.Description;

            await _context.SaveChangesAsync();
        }

        public async Task<Contact> GetOneContact(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
