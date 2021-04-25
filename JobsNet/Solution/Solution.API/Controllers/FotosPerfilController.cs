using data = Solution.DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Solution.DAL.EF;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosPerfilController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        public FotosPerfilController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FotosPerfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.FotosPerfil>>> GetFotosPerfil()
        {
            return _mapper.Map<IEnumerable<DataModels.FotosPerfil>>(
                await new BS.FotoPerfil(_context).GetAllIncludeWithAsync()
            ).ToList();
        }

        // GET: api/FotosPerfil/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<data.FotosPerfil>> GetFotosPerfil(int id)
        // {
        //     var fotosPerfil = await _context.FotosPerfil.FindAsync(id);

        //     if (fotosPerfil == null)
        //     {
        //         return NotFound();
        //     }

        //     return fotosPerfil;
        // }

        // PUT: api/FotosPerfil/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutFotosPerfil(int id, FotosPerfil fotosPerfil)
        // {
        //     if (id != fotosPerfil.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(fotosPerfil).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!FotosPerfilExist(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/FotosPerfil
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.FotosPerfil>> PostFotosPerfil(DataModels.FotosPerfil fotosPerfil)
        {
            try
            {
                new BS.FotoPerfil(_context).Insert(
                    _mapper.Map<data.FotosPerfil>(fotosPerfil)
                );
            }
            catch (Exception)
            {
                if (FotosPerfilExist(fotosPerfil.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetFotosPerfil", new { id = fotosPerfil.Id }, fotosPerfil);
        }

        // // DELETE: api/FotosPerfil/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<FotosPerfil>> DeleteFotosPerfil(int id)
        // {
        //     var fotosPerfil = await _context.FotosPerfil.FindAsync(id);
        //     if (fotosPerfil == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.FotosPerfil.Remove(fotosPerfil);
        //     await _context.SaveChangesAsync();

        //     return fotosPerfil;
        // }

        private bool FotosPerfilExist(int id)
        {
            return (new BS.FotoPerfil(_context).GetOneById(id)) != null;
        }
    }
}
