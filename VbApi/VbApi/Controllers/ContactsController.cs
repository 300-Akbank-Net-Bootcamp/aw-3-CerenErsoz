using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VbApi.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly VbDbContext _dbContext;

        public ContactsController(VbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<Contact>> Get()
        {
            return await _dbContext.Set<Contact>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            var contact = await _dbContext.Set<Contact>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            await _dbContext.Set<Contact>().AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Contact contact)
        {
            var fromDb = await _dbContext.Set<Contact>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            fromDb.ContactType = contact.ContactType;
            fromDb.Information = contact.Information;
            fromDb.IsDefault = contact.IsDefault;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _dbContext.Set<Contact>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            _dbContext.Set<Contact>().Remove(fromDb);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
