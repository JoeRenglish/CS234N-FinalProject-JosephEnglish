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
    public class MashController : ControllerBase
    {
        private readonly BitsContext _context;

        public MashController(BitsContext context)
        {
            _context = context;
        }

        // GET: api/Mash
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mash>>> GetMashes()
        {
          if (_context.Mashes == null)
          {
              return NotFound();
          }
            return await _context.Mashes.ToListAsync();
        }

        // GET: api/Mash/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mash>> GetMash(int id)
        {
          if (_context.Mashes == null)
          {
              return NotFound();
          }
            var mash = await _context.Mashes.FindAsync(id);

            if (mash == null)
            {
                return NotFound();
            }

            return mash;
        }

        // PUT: api/Mash/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMash(int id, Mash mash)
        {
            if (id != mash.MashId)
            {
                return BadRequest();
            }

            _context.Entry(mash).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MashExists(id))
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

        // POST: api/Mash
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mash>> PostMash(Mash mash)
        {
          if (_context.Mashes == null)
          {
              return Problem("Entity set 'BitsContext.Mashes'  is null.");
          }
            _context.Mashes.Add(mash);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMash", new { id = mash.MashId }, mash);
        }

        // DELETE: api/Mash/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMash(int id)
        {
            if (_context.Mashes == null)
            {
                return NotFound();
            }
            var mash = await _context.Mashes.FindAsync(id);
            if (mash == null)
            {
                return NotFound();
            }

            _context.Mashes.Remove(mash);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MashExists(int id)
        {
            return (_context.Mashes?.Any(e => e.MashId == id)).GetValueOrDefault();
        }
    }
}
