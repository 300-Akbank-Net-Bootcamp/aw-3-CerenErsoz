using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VbApi.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EftTransactionsController : ControllerBase
    {
        private readonly VbDbContext _dbContext;

        public EftTransactionsController(VbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<EftTransaction>> Get()
        {
            return await _dbContext.Set<EftTransaction>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EftTransaction>> Get(int id)
        {
            var eftTransaction = await _dbContext.Set<EftTransaction>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (eftTransaction == null)
            {
                return NotFound();
            }

            return eftTransaction;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EftTransaction eftTransaction)
        {
            await _dbContext.Set<EftTransaction>().AddAsync(eftTransaction);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = eftTransaction.Id }, eftTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EftTransaction eftTransaction)
        {
            var fromDb = await _dbContext.Set<EftTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            fromDb.ReferenceNumber = eftTransaction.ReferenceNumber;
            fromDb.TransactionDate = eftTransaction.TransactionDate;
            fromDb.Amount = eftTransaction.Amount;
            fromDb.Description = eftTransaction.Description;
            fromDb.SenderAccount = eftTransaction.SenderAccount;
            fromDb.SenderIban = eftTransaction.SenderIban;
            fromDb.SenderName = eftTransaction.SenderName;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _dbContext.Set<EftTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return NotFound();
            }

            _dbContext.Set<EftTransaction>().Remove(fromDb);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
