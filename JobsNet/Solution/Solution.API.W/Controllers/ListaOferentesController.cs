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
    public class ListaOferentesController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public ListaOferentesController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/ListaOferentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaOferentes>>> GetListaOferentes()
        {
            return await _context.ListaOferentes.ToListAsync();
        }

        // GET: api/ListaOferentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListaOferentes>> GetListaOferentes(decimal id)
        {
            var listaOferentes = await _context.ListaOferentes.FindAsync(id);

            if (listaOferentes == null)
            {
                return NotFound();
            }

            return listaOferentes;
        }

        // PUT: api/ListaOferentes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListaOferentes(decimal id, ListaOferentes listaOferentes)
        {
            if (id != listaOferentes.IdOferente)
            {
                return BadRequest();
            }

            _context.Entry(listaOferentes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaOferentesExists(id))
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

        // POST: api/ListaOferentes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ListaOferentes>> PostListaOferentes(ListaOferentes listaOferentes)
        {
            _context.ListaOferentes.Add(listaOferentes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ListaOferentesExists(listaOferentes.IdOferente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetListaOferentes", new { id = listaOferentes.IdOferente }, listaOferentes);
        }

        // DELETE: api/ListaOferentes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ListaOferentes>> DeleteListaOferentes(decimal id)
        {
            var listaOferentes = await _context.ListaOferentes.FindAsync(id);
            if (listaOferentes == null)
            {
                return NotFound();
            }

            _context.ListaOferentes.Remove(listaOferentes);
            await _context.SaveChangesAsync();

            return listaOferentes;
        }

        private bool ListaOferentesExists(decimal id)
        {
            return _context.ListaOferentes.Any(e => e.IdOferente == id);
        }
    }
}
