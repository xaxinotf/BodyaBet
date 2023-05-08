using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BodyaBet.Contexts;
using BodyaBet.Models;

namespace BodyaBet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly VolleyballContext _context;

        public MatchesController(VolleyballContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matches>>> GetMatches()
        {
          if (_context.Matches == null)
          {
              return NotFound();
          }
            return await _context.Matches.ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matches>> GetMatches(int id)
        {
          if (_context.Matches == null)
          {
              return NotFound();
          }
            var matches = await _context.Matches.FindAsync(id);

            if (matches == null)
            {
                return NotFound();
            }

            return matches;
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatches(int id, Matches matches)
        {
            if (id != matches.Id)
            {
                return BadRequest();
            }

            _context.Entry(matches).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchesExists(id))
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

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matches>> PostMatches(Matches matches)
        {
          if (_context.Matches == null)
          {
              return Problem("Entity set 'VolleyballContext.Matches'  is null.");
          }
            _context.Matches.Add(matches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatches", new { id = matches.Id }, matches);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatches(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var matches = await _context.Matches.FindAsync(id);
            if (matches == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(matches);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchesExists(int id)
        {
            return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
