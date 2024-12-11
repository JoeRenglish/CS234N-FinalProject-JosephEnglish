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
    public class BarrelController : ControllerBase
    {
        private readonly BitsContext _context;

        public BarrelController(BitsContext context)
        {
            _context = context;
        }

        // GET: api/Barrel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barrel>>> GetBarrels()
        {
          if (_context.Barrels == null)
          {
              return NotFound();
          }
            return await _context.Barrels.ToListAsync();
        }

        // GET: api/Barrel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barrel>> GetBarrel(int id)
        {
          if (_context.Barrels == null)
          {
              return NotFound();
          }
            var barrel = await _context.Barrels.FindAsync(id);

            if (barrel == null)
            {
                return NotFound();
            }

            return barrel;
        }

        // PUT: api/Barrel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarrel(int id, Barrel barrel)
        {
            if (id != barrel.BrewContainerId)
            {
                return BadRequest();
            }

            _context.Entry(barrel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarrelExists(id))
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

        // POST: api/Barrel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Barrel>> PostBarrel(Barrel barrel)
        {
          if (_context.Barrels == null)
          {
              return Problem("Entity set 'BitsContext.Barrels'  is null.");
          }
            _context.Barrels.Add(barrel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BarrelExists(barrel.BrewContainerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBarrel", new { id = barrel.BrewContainerId }, barrel);
        }

        // DELETE: api/Barrel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarrel(int id)
        {
            if (_context.Barrels == null)
            {
                return NotFound();
            }
            var barrel = await _context.Barrels.FindAsync(id);
            if (barrel == null)
            {
                return NotFound();
            }

            _context.Barrels.Remove(barrel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarrelExists(int id)
        {
            return (_context.Barrels?.Any(e => e.BrewContainerId == id)).GetValueOrDefault();
        }
    }
}
