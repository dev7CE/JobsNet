using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.API.W.Models;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CantonesController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public CantonesController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/Cantones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cantones>>> GetCantones()
        {
            return await _context.Cantones.ToListAsync();
        }

        // GET: api/Cantones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cantones>> GetCantones(decimal id)
        {
            var cantones = await _context.Cantones.FindAsync(id);

            if (cantones == null)
            {
                return NotFound();
            }

            return cantones;
        }

        // PUT: api/Cantones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCantones(decimal id, Cantones cantones)
        {
            if (id != cantones.IdCanton)
            {
                return BadRequest();
            }

            _context.Entry(cantones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CantonesExists(id))
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

        // POST: api/Cantones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cantones>> PostCantones(Cantones cantones)
        {
            _context.Cantones.Add(cantones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCantones", new { id = cantones.IdCanton }, cantones);
        }

        // DELETE: api/Cantones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cantones>> DeleteCantones(decimal id)
        {
            var cantones = await _context.Cantones.FindAsync(id);
            if (cantones == null)
            {
                return NotFound();
            }

            _context.Cantones.Remove(cantones);
            await _context.SaveChangesAsync();

            return cantones;
        }

        private bool CantonesExists(decimal id)
        {
            return _context.Cantones.Any(e => e.IdCanton == id);
        }
    }
}
