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
    public class DocumentosController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public DocumentosController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentos>>> GetDocumentos()
        {
            return await _context.Documentos.ToListAsync();
        }

        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documentos>> GetDocumentos(int id)
        {
            var documentos = await _context.Documentos.FindAsync(id);

            if (documentos == null)
            {
                return NotFound();
            }

            return documentos;
        }

        // PUT: api/Documentos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentos(int id, Documentos documentos)
        {
            if (id != documentos.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentosExists(id))
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

        // POST: api/Documentos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Documentos>> PostDocumentos(Documentos documentos)
        {
            _context.Documentos.Add(documentos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentos", new { id = documentos.Id }, documentos);
        }

        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Documentos>> DeleteDocumentos(int id)
        {
            var documentos = await _context.Documentos.FindAsync(id);
            if (documentos == null)
            {
                return NotFound();
            }

            _context.Documentos.Remove(documentos);
            await _context.SaveChangesAsync();

            return documentos;
        }

        private bool DocumentosExists(int id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
