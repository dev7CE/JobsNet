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
    public class OferentesController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public OferentesController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/Oferentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oferentes>>> GetOferentes()
        {
            return await _context.Oferentes.ToListAsync();
        }

        // GET: api/Oferentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferentes>> GetOferentes(decimal id)
        {
            var oferentes = await _context.Oferentes.FindAsync(id);

            if (oferentes == null)
            {
                return NotFound();
            }

            return oferentes;
        }

        // PUT: api/Oferentes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferentes(decimal id, Oferentes oferentes)
        {
            if (id != oferentes.IdOferente)
            {
                return BadRequest();
            }

            _context.Entry(oferentes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OferentesExists(id))
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

        // POST: api/Oferentes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Oferentes>> PostOferentes(Oferentes oferentes)
        {
            _context.Oferentes.Add(oferentes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOferentes", new { id = oferentes.IdOferente }, oferentes);
        }

        // DELETE: api/Oferentes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oferentes>> DeleteOferentes(decimal id)
        {
            var oferentes = await _context.Oferentes.FindAsync(id);
            if (oferentes == null)
            {
                return NotFound();
            }

            _context.Oferentes.Remove(oferentes);
            await _context.SaveChangesAsync();

            return oferentes;
        }

        private bool OferentesExists(decimal id)
        {
            return _context.Oferentes.Any(e => e.IdOferente == id);
        }
    }
}
