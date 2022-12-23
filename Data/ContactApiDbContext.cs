using Microsoft.EntityFrameworkCore;
using WebApi_Assignment.Model;

namespace WebApi_Assignment.Data
{
    public class ContactApiDbContext : DbContext
    {
        public ContactApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
