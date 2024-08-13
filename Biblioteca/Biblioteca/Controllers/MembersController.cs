using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Data;
using Biblioteca.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly LibraryContext _context;

    public MembersController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        return await _context.Members.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetMember(int id)
    {
        var member = await _context.Members.FindAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        return member;
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Member>>> SearchMembers([FromQuery] string name)
    {
        var query = _context.Members.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(m => m.Name.Contains(name));
        }

        return await query.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Member>> PostMember(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMember", new { id = member.Id }, member);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMember(int id, Member member)
    {
        if (id != member.Id)
        {
            return BadRequest();
        }

        _context.Entry(member).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MemberExists(id))
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
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
        {
            return NotFound();
        }

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MemberExists(int id)
    {
        return _context.Members.Any(e => e.Id == id);
    }
}


