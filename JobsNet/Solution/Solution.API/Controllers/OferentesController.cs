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
    public class OferentesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public OferentesController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Oferentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Oferentes>>> GetOferentes()
        {
            return _mapper.Map<IEnumerable<DataModels.Oferentes>>(
                await new BS.Oferente(_context).GetAllIncludeWithAsync()
            ).ToList();
        }

        // GET: api/Oferentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Oferentes>> GetOferentes(int id)
        {
            var oferentes = await new BS.Oferente(_context).GetOneByIdIncludeWihAsync(id);

            if (oferentes == null)
            {
                return NotFound();
            }

            return _mapper.Map<DataModels.Oferentes>(oferentes);
        }
        //
        // PUT: api/Oferentes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferentes(int id, DataModels.Oferentes oferentes)
        {
            if (id != oferentes.IdOferente)
            return BadRequest();

            try
            {
                new BS.Oferente(_context)
                    .Update(_mapper.Map<data.Oferentes>(oferentes));
            }
            catch (Exception)
            {
                if (!OferentesExists(id))
                { return NotFound(); }
                else
                { throw; }
            }
            return NoContent();
        }
        //
        // POST: api/Oferentes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Oferentes>> PostOferentes(DataModels.Oferentes oferentes)
        {
            try
            {
                new BS.Oferente(_context)
                    .Insert(_mapper.Map<data.Oferentes>(oferentes));
            }
            catch (System.Exception)
            {
                throw;
            }
            return CreatedAtAction("GetOferentes", new { id = oferentes.IdOferente }, oferentes);
        }
        //
        // DELETE: api/Oferentes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Oferentes>> DeleteOferentes(int id)
        {
            var oferentes = new BS.Oferente(_context).GetOneById(id);
            if (oferentes == null)
            return NotFound();
            
            new BS.Oferente(_context).Delete(oferentes);
        
            return _mapper.Map<DataModels.Oferentes>(oferentes);
        }
        
        private bool OferentesExists(int id)
        {
            return (new BS.Oferente(_context).GetOneById(id) != null);
        }
    }
}
