using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution.DAL.EF;
using data = Solution.DO.Objects;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaOferentesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public ListaOferentesController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ListaOferentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.ListaOferentes>>> GetListaOferentes()
        {
            return _mapper.Map<IEnumerable<DataModels.ListaOferentes>>(
                await new BS.ListaOferente(_context).GetAllIncludeWithAsync()
            ).ToList();
        }

        // GET: api/ListaOferentes/5
        [HttpGet("{idOferente}/{idPuesto}")]
        public async Task<ActionResult<DataModels.ListaOferentes>> GetListaOferentes(int idOferente, int idPuesto)
        {
            var listaOferentes = await new BS.ListaOferente(_context)
                .GetOneByIdsIncludeWihAsync(idOferente, idPuesto);

            if (listaOferentes == null)
            return NotFound();
            
            return _mapper.Map<DataModels.ListaOferentes>(listaOferentes);
        }
        //
        // PUT: api/ListaOferentes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{idOferente}/{idPuesto}")]
        public async Task<IActionResult> PutListaOferentes(int idOferente, int idPuesto, DataModels.ListaOferentes listaOferentes)
        {
            if (idOferente != listaOferentes.IdOferente
                || idPuesto != listaOferentes.IdPuesto)
            return BadRequest();
            
            try
            {
                new BS.ListaOferente(_context).Update(
                    _mapper.Map<data.ListaOferentes>(listaOferentes)
                );
            }
            catch (Exception)
            {
                if (!ListaOferentesExists(idOferente, idPuesto))
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
        //
        // POST: api/ListaOferentes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.ListaOferentes>> PostListaOferentes(DataModels.ListaOferentes listaOferentes)
        {
            try
            {
                new BS.ListaOferente(_context).Insert(
                    _mapper.Map<data.ListaOferentes>(listaOferentes)
                );
            }
            catch (Exception)
            {
                if (ListaOferentesExists(listaOferentes.IdOferente, listaOferentes.IdPuesto))
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
        //
        // DELETE: api/ListaOferentes/5
        [HttpDelete("{idOferente}/{idPuesto}")]
        public async Task<ActionResult<DataModels.ListaOferentes>> DeleteListaOferentes
            (int idOferente, int idPuesto)
        {
            var listaOferentes = new BS.ListaOferente(_context)
                .GetOne(lo => 
                (lo.IdPuesto == idPuesto && lo.IdOferente == idOferente));
            if (listaOferentes == null)
            return NotFound();
            
            new BS.ListaOferente(_context).Delete(listaOferentes);
            return _mapper.Map<DataModels.ListaOferentes>(listaOferentes);
        }
        
        private bool ListaOferentesExists(int idOferente, int idPuesto)
        {
            return (new BS.ListaOferente(_context).GetOne(lo => 
                (lo.IdPuesto == idPuesto && lo.IdOferente == idOferente)) 
                != null);
        }
    }
}
