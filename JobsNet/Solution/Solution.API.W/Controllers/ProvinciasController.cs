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
    public class ProvinciasController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public ProvinciasController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/Provincias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincias>>> GetProvincias()
        {
            return await _context.Provincias.ToListAsync();
        }

        // GET: api/Provincias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provincias>> GetProvincias(int id)
        {
            var provincias = await _context.Provincias.FindAsync(id);

            if (provincias == null)
            {
                return NotFound();
            }

            return provincias;
        }

        // PUT: api/Provincias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvincias(int id, Provincias provincias)
        {
            if (id != provincias.IdProvincia)
            {
                return BadRequest();
            }

            _context.Entry(provincias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinciasExists(id))
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

        // POST: api/Provincias
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Provincias>> PostProvincias(Provincias provincias)
        {
            _context.Provincias.Add(provincias);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProvinciasExists(provincias.IdProvincia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProvincias", new { id = provincias.IdProvincia }, provincias);
        }

        // DELETE: api/Provincias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Provincias>> DeleteProvincias(int id)
        {
            var provincias = await _context.Provincias.FindAsync(id);
            if (provincias == null)
            {
                return NotFound();
            }

            _context.Provincias.Remove(provincias);
            await _context.SaveChangesAsync();

            return provincias;
        }

        private bool ProvinciasExists(int id)
        {
            return _context.Provincias.Any(e => e.IdProvincia == id);
        }
    }
}
