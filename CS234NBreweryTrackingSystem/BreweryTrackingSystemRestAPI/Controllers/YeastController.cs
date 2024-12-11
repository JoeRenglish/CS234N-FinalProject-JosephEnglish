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
    public class YeastController : ControllerBase
    {
        private readonly BitsContext _context;

        public YeastController(BitsContext context)
        {
            _context = context;
        }

        // GET: api/Yeast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Yeast>>> GetYeasts()
        {
          if (_context.Yeasts == null)
          {
              return NotFound();
          }
            return await _context.Yeasts.ToListAsync();
        }

        // GET: api/Yeast/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Yeast>> GetYeast(int id)
        {
          if (_context.Yeasts == null)
          {
              return NotFound();
          }
            var yeast = await _context.Yeasts.FindAsync(id);

            if (yeast == null)
            {
                return NotFound();
            }

            return yeast;
        }

        // PUT: api/Yeast/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYeast(int id, Yeast yeast)
        {
            if (id != yeast.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(yeast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YeastExists(id))
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

        // POST: api/Yeast
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Yeast>> PostYeast(Yeast yeast)
        {
          if (_context.Yeasts == null)
          {
              return Problem("Entity set 'BitsContext.Yeasts'  is null.");
          }
            _context.Yeasts.Add(yeast);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (YeastExists(yeast.IngredientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetYeast", new { id = yeast.IngredientId }, yeast);
        }

        // DELETE: api/Yeast/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYeast(int id)
        {
            if (_context.Yeasts == null)
            {
                return NotFound();
            }
            var yeast = await _context.Yeasts.FindAsync(id);
            if (yeast == null)
            {
                return NotFound();
            }

            _context.Yeasts.Remove(yeast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YeastExists(int id)
        {
            return (_context.Yeasts?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }
    }
}
