using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using DOObjects = Solution.DO.Objects;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private IMapper _mapper;

        public ProvinciasController(SolutionDbContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Provincias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Provincias>>> GetProvincias()
        {
            //var result = new BS.Provincia(_context).GetAll();

            var provincias = _mapper.Map<IEnumerable<DataModels.Provincias>>(
                new BS.Provincia(_context).GetAll()
            ).ToList();  
            
            return provincias;
        }

        // GET: api/Provincias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Provincias>> GetProvincias(int id)
        {
            var result = new BS.Provincia(_context).GetOneById(id);

            if (result == null)
            return NotFound();
            
            var mappAux = _mapper.Map<DataModels.Provincias>(result);
             
            return mappAux;
        }

        // PUT: api/Provincias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvincias(int id, DataModels.Provincias provincias)
        {
            if (id != provincias.IdProvincia)
            return BadRequest();
            
            try
            {
                new BS.Provincia(_context).Update(
                _mapper.Map<DOObjects.Provincias>(provincias)
                );
            }
            catch (Exception)
            {
                if (!ProvinciasExists(id))
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

        // POST: api/Provincias
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Provincias>> PostProvincias(DataModels.Provincias provincias)
        {
            try
            {
                new BS.Provincia(_context).Insert(
                        _mapper.Map<DOObjects.Provincias>(provincias)
                );
            }
            catch (Exception)
            {
                if (ProvinciasExists(provincias.IdProvincia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetProvincias", new { id = provincias.IdProvincia }, provincias);
        }

        // DELETE: api/Provincias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Provincias>> DeleteProvincias(int id)
        {
            var provincias = new BS.Provincia(_context).GetOneById(id);
            if (provincias == null)
            return NotFound();
            
            new BS.Provincia(_context).Delete(provincias);

            return _mapper.Map<DataModels.Provincias>(provincias);
        }

        private bool ProvinciasExists(int id)
        {
            return (new BS.Provincia(_context).GetOneById(id) != null);
        }
    }
}
