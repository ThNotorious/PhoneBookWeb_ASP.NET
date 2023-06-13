using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBookWeb.Models;

namespace PhoneBookWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Contact> DataContacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}