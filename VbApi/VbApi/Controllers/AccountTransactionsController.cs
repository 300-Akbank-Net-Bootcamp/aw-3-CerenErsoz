using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VbApi.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly VbDbContext _dbContext;

        public AccountTransactionsController(VbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<AccountTransaction>> Get()
        {
            return await _dbContext.Set<AccountTransaction>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountTransaction>> Get(int id)
        {
            var transaction = await _dbContext.Set<AccountTransaction>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountTransaction transaction)
        {
            await _dbContext.Set<AccountTransaction>().AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountTransaction transaction)
        {
            var fromDb = await _dbContext.Set<AccountTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            fromDb.ReferenceNumber = transaction.ReferenceNumber;
            fromDb.TransactionDate = transaction.TransactionDate;
            fromDb.Amount = transaction.Amount;
            fromDb.Description = transaction.Description;
            fromDb.TransferType = transaction.TransferType;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _dbContext.Set<AccountTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            _dbContext.Set<AccountTransaction>().Remove(fromDb);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
