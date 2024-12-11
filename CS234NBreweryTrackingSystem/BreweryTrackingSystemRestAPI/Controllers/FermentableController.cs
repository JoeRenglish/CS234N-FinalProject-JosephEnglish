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
    public class FermentableController : ControllerBase
    {
        private readonly BitsContext _context;

        public FermentableController(BitsContext context)
        {
            _context = context;
        }

        // GET: api/Fermentable
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fermentable>>> GetFermentables()
        {
          if (_context.Fermentables == null)
          {
              return NotFound();
          }
            return await _context.Fermentables.ToListAsync();
        }

        // GET: api/Fermentable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fermentable>> GetFermentable(int id)
        {
          if (_context.Fermentables == null)
          {
              return NotFound();
          }
            var fermentable = await _context.Fermentables.FindAsync(id);

            if (fermentable == null)
            {
                return NotFound();
            }

            return fermentable;
        }

        // PUT: api/Fermentable/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFermentable(int id, Fermentable fermentable)
        {
            if (id != fermentable.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(fermentable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FermentableExists(id))
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

        // POST: api/Fermentable
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fermentable>> PostFermentable(Fermentable fermentable)
        {
          if (_context.Fermentables == null)
          {
              return Problem("Entity set 'BitsContext.Fermentables'  is null.");
          }
            _context.Fermentables.Add(fermentable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FermentableExists(fermentable.IngredientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFermentable", new { id = fermentable.IngredientId }, fermentable);
        }

        // DELETE: api/Fermentable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFermentable(int id)
        {
            if (_context.Fermentables == null)
            {
                return NotFound();
            }
            var fermentable = await _context.Fermentables.FindAsync(id);
            if (fermentable == null)
            {
                return NotFound();
            }

            _context.Fermentables.Remove(fermentable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FermentableExists(int id)
        {
            return (_context.Fermentables?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }
    }
}
