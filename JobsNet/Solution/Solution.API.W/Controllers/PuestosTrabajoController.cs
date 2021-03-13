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
    public class PuestosTrabajoController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public PuestosTrabajoController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/PuestosTrabajo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuestosTrabajo>>> GetPuestosTrabajo()
        {
            return await _context.PuestosTrabajo.ToListAsync();
        }

        // GET: api/PuestosTrabajo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PuestosTrabajo>> GetPuestosTrabajo(decimal id)
        {
            var puestosTrabajo = await _context.PuestosTrabajo.FindAsync(id);

            if (puestosTrabajo == null)
            {
                return NotFound();
            }

            return puestosTrabajo;
        }

        // PUT: api/PuestosTrabajo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuestosTrabajo(decimal id, PuestosTrabajo puestosTrabajo)
        {
            if (id != puestosTrabajo.IdPuesto)
            {
                return BadRequest();
            }

            _context.Entry(puestosTrabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestosTrabajoExists(id))
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

        // POST: api/PuestosTrabajo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PuestosTrabajo>> PostPuestosTrabajo(PuestosTrabajo puestosTrabajo)
        {
            _context.PuestosTrabajo.Add(puestosTrabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuestosTrabajo", new { id = puestosTrabajo.IdPuesto }, puestosTrabajo);
        }

        // DELETE: api/PuestosTrabajo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PuestosTrabajo>> DeletePuestosTrabajo(decimal id)
        {
            var puestosTrabajo = await _context.PuestosTrabajo.FindAsync(id);
            if (puestosTrabajo == null)
            {
                return NotFound();
            }

            _context.PuestosTrabajo.Remove(puestosTrabajo);
            await _context.SaveChangesAsync();

            return puestosTrabajo;
        }

        private bool PuestosTrabajoExists(decimal id)
        {
            return _context.PuestosTrabajo.Any(e => e.IdPuesto == id);
        }
    }
}
