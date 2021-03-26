using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using data = Solution.DO.Objects;  

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public EmpresasController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Empresas>>> GetEmpresas()
        {
            return _mapper.Map<IEnumerable<DataModels.Empresas>>(
                await new BS.Empresa(_context).GetAllIncludeWithAsync()
            ).ToList();
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Empresas>> GetEmpresas(int id)
        {
            var empresas = await new BS.Empresa(_context).GetOneByIdIncludeWihAsync(id);

            if (empresas == null)
            return NotFound();

            return _mapper.Map<DataModels.Empresas>(empresas);
        }
        //
        // PUT: api/Empresas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresas(int id, DataModels.Empresas empresas)
        {
            if (id != empresas.IdEmpresa)
            return BadRequest();
            
        
            try
            {
                new BS.Empresa(_context).Update(_mapper.Map<data.Empresas>(empresas));
            }
            catch (Exception)
            {
                if (!EmpresasExists(id))
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
        // POST: api/Empresas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Empresas>> PostEmpresas(DataModels.Empresas empresas)
        {
            try
            {
                new BS.Empresa(_context).Insert(_mapper.Map<data.Empresas>(empresas));
            }
            catch (Exception)
            {
                if (EmpresasExists(empresas.IdEmpresa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetEmpresas", new { id = empresas.IdEmpresa }, empresas);
        }
        //
        // DELETE: api/Empresas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Empresas>> DeleteEmpresas(int id)
        {
            var empresas = new BS.Empresa(_context).GetOneById(id);
            if (empresas == null)
            {
                return NotFound();
            }
        
            new BS.Empresa(_context).Delete(empresas);
        
            return _mapper.Map<DataModels.Empresas>(empresas);
        }
        //
        private bool EmpresasExists(int id)
        {
            return (new BS.Empresa(_context).GetOneById(id) != null);
        }
    }
}
