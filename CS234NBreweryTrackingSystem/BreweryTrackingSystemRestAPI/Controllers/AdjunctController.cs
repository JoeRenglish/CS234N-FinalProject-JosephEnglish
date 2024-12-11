using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryTrackingSystem.models;

namespace BreweryTrackingSystemRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjunctController : ControllerBase
    {
        private readonly BitsContext _context;

        public AdjunctController(BitsContext context)
        {
            _context = context;
        }

        // GET: api/Adjunct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adjunct>>> GetAdjuncts()
        {
          if (_context.Adjuncts == null)
          {
              return NotFound();
          }
            return await _context.Adjuncts.ToListAsync();
        }

        // GET: api/Adjunct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adjunct>> GetAdjunct(int id)
        {
          if (_context.Adjuncts == null)
          {
              return NotFound();
          }
            var adjunct = await _context.Adjuncts.FindAsync(id);

            if (adjunct == null)
            {
                return NotFound();
            }

            return adjunct;
        }

        // PUT: api/Adjunct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdjunct(int id, Adjunct adjunct)
        {
            if (id != adjunct.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(adjunct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdjunctExists(id))
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

        // POST: api/Adjunct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Adjunct>> PostAdjunct(Adjunct adjunct)
        {
          if (_context.Adjuncts == null)
          {
              return Problem("Entity set 'BitsContext.Adjuncts'  is null.");
          }
            _context.Adjuncts.Add(adjunct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdjunctExists(adjunct.IngredientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdjunct", new { id = adjunct.IngredientId }, adjunct);
        }

        // DELETE: api/Adjunct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdjunct(int id)
        {
            if (_context.Adjuncts == null)
            {
                return NotFound();
            }
            var adjunct = await _context.Adjuncts.FindAsync(id);
            if (adjunct == null)
            {
                return NotFound();
            }

            _context.Adjuncts.Remove(adjunct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdjunctExists(int id)
        {
            return (_context.Adjuncts?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }
    }
}
