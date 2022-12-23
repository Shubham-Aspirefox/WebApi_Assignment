using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Assignment.Data;
using WebApi_Assignment.Model;

namespace WebApi_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactApiDbContext dbContext;

        public ContactsController(ContactApiDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);

        }
        [HttpPost]
        public async Task<IActionResult > AddContacts(addContacts addContacts)
        {
            Contact contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContacts.Address,
                FullName = addContacts.FullName,
                Email = addContacts.Email, 
                Phone = addContacts.Phone,
            };
           await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContact)
        {
            var contact= await dbContext.Contacts.FindAsync(id);
            if(contact!=null)
            {
                contact.FullName = updateContact.FullName;
                contact.Email = updateContact.Email;
                contact.Phone = updateContact.Phone;    
                contact.Address = updateContact.Address;

               await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact=await dbContext.Contacts .FindAsync(id);
            if( contact!=null)
            {
                dbContext.Contacts.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    }
}
