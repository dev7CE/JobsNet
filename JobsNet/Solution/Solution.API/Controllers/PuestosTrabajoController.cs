using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution.DAL.EF;
using data = Solution.DO.Objects;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestosTrabajoController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public PuestosTrabajoController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PuestosTrabajo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.PuestosTrabajo>>> GetPuestosTrabajo()
        {
            return _mapper.Map<IEnumerable<DataModels.PuestosTrabajo>>(
                await new BS.PuestosTrabajo(_context).GetAllIncludeWithAsync()
            ).ToList();
        }

        // GET: api/PuestosTrabajo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.PuestosTrabajo>> GetPuestosTrabajo(int id)
        {
            var puestosTrabajo = await new BS.PuestosTrabajo(_context).GetOneByIdIncludeWihAsync(id);

            if (puestosTrabajo == null)
            return NotFound();

            return _mapper.Map<DataModels.PuestosTrabajo>(puestosTrabajo);
        }
        //
        // PUT: api/PuestosTrabajo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuestosTrabajo(int id, DataModels.PuestosTrabajo puestosTrabajo)
        {
            if (id != puestosTrabajo.IdPuesto)
            return BadRequest();
            
            try
            {
                new BS.PuestosTrabajo(_context).Update(
                    _mapper.Map<data.PuestosTrabajo>(puestosTrabajo)
                );
            }
            catch (Exception)
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
        //
        // POST: api/PuestosTrabajo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.PuestosTrabajo>> PostPuestosTrabajo(DataModels.PuestosTrabajo puestosTrabajo)
        {
            try
            {
                new BS.PuestosTrabajo(_context).Insert(
                    _mapper.Map<data.PuestosTrabajo>(puestosTrabajo)
                );
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            return CreatedAtAction("GetPuestosTrabajo", new { id = puestosTrabajo.IdPuesto }, puestosTrabajo);
        }
        //
        // DELETE: api/PuestosTrabajo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.PuestosTrabajo>> DeletePuestosTrabajo(int id)
        {
            var puestosTrabajo = new BS.PuestosTrabajo(_context).GetOneById(id);
            if (puestosTrabajo == null)
            return NotFound();
            
            new BS.PuestosTrabajo(_context).Delete(puestosTrabajo);

            return _mapper.Map<DataModels.PuestosTrabajo>(puestosTrabajo);
        }
        
        private bool PuestosTrabajoExists(int id)
        {
            return (new BS.PuestosTrabajo(_context).GetOneById(id) != null);
        }
    }
}
