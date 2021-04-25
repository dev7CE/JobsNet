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
        //
        // GET: api/FotosPerfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.FotosPerfil>> GetFotosPerfil(int id)
        {
            var fotosPerfil = await new BS.FotoPerfil(_context)
                .GetOneByIdIncludeWihAsync(id);

            if (fotosPerfil == null)
            return NotFound();

            return _mapper.Map<DataModels.FotosPerfil>(fotosPerfil);
        }
        //
        // PUT: api/FotosPerfil/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotosPerfil(int id, DataModels.FotosPerfil fotosPerfil)
        {
            if (id != fotosPerfil.Id)
            return BadRequest();
            
            try
            {
                new BS.FotoPerfil(_context).Update(
                    _mapper.Map<data.FotosPerfil>(fotosPerfil)
                );
            }
            catch (Exception)
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

        // DELETE: api/FotosPerfil/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.FotosPerfil>> DeleteFotosPerfil(int id)
        {
            var fotosPerfil = new BS.FotoPerfil(_context).GetOneById(id);
            if (fotosPerfil == null)
            return NotFound();

            new BS.FotoPerfil(_context).Delete(fotosPerfil);

            return _mapper.Map<DataModels.FotosPerfil>(fotosPerfil);
        }

        private bool FotosPerfilExist(int id)
        {
            return (new BS.FotoPerfil(_context).GetOneById(id)) != null;
        }
    }
}
