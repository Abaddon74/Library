using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Data;
using Biblioteca.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly LibraryContext _context;

    public LoansController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
    {
        return await _context.Loans.Include(l => l.Book).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Loan>> GetLoan(int id)
    {
        var loan = await _context.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == id);

        if (loan == null)
        {
            return NotFound();
        }

        return loan;
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoanHistory()
    {
        return await _context.Loans.Include(l => l.Book).Where(l => l.ReturnDate.HasValue).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLoan(int id, Loan loan)
    {
        if (id != loan.Id)
        {
            return BadRequest();
        }

        _context.Entry(loan).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LoanExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
        {
            return NotFound();
        }

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LoanExists(int id)
    {
        return _context.Loans.Any(e => e.Id == id);
    }
}
