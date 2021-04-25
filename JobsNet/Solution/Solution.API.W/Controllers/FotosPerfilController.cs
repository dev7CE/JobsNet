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
    public class FotosPerfilController : ControllerBase
    {
        private readonly JobsNetDbContext _context;

        public FotosPerfilController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: api/FotosPerfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FotosPerfil>>> GetFotosPerfil()
        {
            return await _context.FotosPerfil.ToListAsync();
        }

        // GET: api/FotosPerfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FotosPerfil>> GetFotosPerfil(int id)
        {
            var fotosPerfil = await _context.FotosPerfil.FindAsync(id);

            if (fotosPerfil == null)
            {
                return NotFound();
            }

            return fotosPerfil;
        }

        // PUT: api/FotosPerfil/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotosPerfil(int id, FotosPerfil fotosPerfil)
        {
            if (id != fotosPerfil.Id)
            {
                return BadRequest();
            }

            _context.Entry(fotosPerfil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotosPerfilExist(id))
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

        // POST: api/FotosPerfil
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FotosPerfil>> PostFotosPerfil(FotosPerfil fotosPerfil)
        {
            _context.FotosPerfil.Add(fotosPerfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFotosPerfil", new { id = fotosPerfil.Id }, fotosPerfil);
        }

        // DELETE: api/FotosPerfil/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FotosPerfil>> DeleteFotosPerfil(int id)
        {
            var fotosPerfil = await _context.FotosPerfil.FindAsync(id);
            if (fotosPerfil == null)
            {
                return NotFound();
            }

            _context.FotosPerfil.Remove(fotosPerfil);
            await _context.SaveChangesAsync();

            return fotosPerfil;
        }

        private bool FotosPerfilExist(int id)
        {
            return _context.FotosPerfil.Any(e => e.Id == id);
        }
    }
}
