using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VbApi.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly VbDbContext _dbContext;

        public AccountsController(VbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<Account>> Get()
        {
            return await _dbContext.Set<Account>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int id)
        {
            var account = await _dbContext.Set<Account>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account account)
        {
            await _dbContext.Set<Account>().AddAsync(account);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Account account)
        {
            var fromDb = await _dbContext.Set<Account>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            fromDb.AccountNumber = account.AccountNumber;
            fromDb.IBAN = account.IBAN;
            fromDb.Balance = account.Balance;
            fromDb.CurrencyType = account.CurrencyType;
            fromDb.Name = account.Name;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _dbContext.Set<Account>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            _dbContext.Set<Account>().Remove(fromDb);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
