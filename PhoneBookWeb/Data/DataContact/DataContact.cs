using Microsoft.EntityFrameworkCore;
using PhoneBookWeb.Data;
using PhoneBookWeb.Interfaces;
using PhoneBookWeb.Models;

internal class DataContact : IDataContact
{
    private readonly ApplicationDbContext _context;

    public DataContact(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Contact>> GetAll()
    {
        return await _context.DataContacts.ToListAsync();
    }

    public async Task<bool> Add(string secondName, string firstName, string middleName, string phoneNumber, string description)
    {
        if (phoneNumber != null && firstName != null)
        {
            _context.DataContacts.Add(
                new Contact()
                {
                    SecondName = secondName,
                    FirstName = firstName,
                    MiddleName = middleName,
                    PhoneNumber = phoneNumber,
                    Description = description
                });
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<Contact> GetOneElement(int idContact)
    {
        Contact contact = _context.DataContacts.FirstOrDefault(item => item.ID == idContact);

        return contact;
    }

    public async Task Delete(int idContact)
    {
        Contact contact = _context.DataContacts.FirstOrDefault(item => item.ID == idContact);

        if (contact != null)
        {
            _context.DataContacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

    }

    public async Task Changed(int idContact, string newSecondName, string newFirstName, string newMiddleName, string newPhoneNumber, string newDescription)
    {
        Contact contact = _context.DataContacts.FirstOrDefault(item => item.ID == idContact);

        if (contact != null)
        {
            if (newPhoneNumber != null && newFirstName != null)
            {
                contact.SecondName = newSecondName;
                contact.FirstName = newFirstName;
                contact.MiddleName = newMiddleName;
                contact.PhoneNumber = newPhoneNumber;
                contact.Description = newDescription;

                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task<IEnumerable<Contact>> SearchByLastName(string secondName)
    {
        return await _context.DataContacts.Where(c => c.SecondName == secondName).ToListAsync();
    }
}
