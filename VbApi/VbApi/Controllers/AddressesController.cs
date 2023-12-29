using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VbApi.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly VbDbContext _dbContext;

        public AddressesController(VbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<Address>> Get()
        {
            return await _dbContext.Set<Address>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(int id)
        {
            var address = await _dbContext.Set<Address>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Address address)
        {
            await _dbContext.Set<Address>().AddAsync(address);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Address address)
        {
            var fromDb = await _dbContext.Set<Address>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            fromDb.Address1 = address.Address1;
            fromDb.Address2 = address.Address2;
            fromDb.Country = address.Country;
            fromDb.City = address.City;
            fromDb.County = address.County;
            fromDb.PostalCode = address.PostalCode;
            fromDb.IsDefault = address.IsDefault;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _dbContext.Set<Address>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            _dbContext.Set<Address>().Remove(fromDb);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
