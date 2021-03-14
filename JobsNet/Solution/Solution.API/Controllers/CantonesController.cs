using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Solution.DAL.EF;
using DOObjects = Solution.DO.Objects;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CantonesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public CantonesController(SolutionDbContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cantones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Cantones>>> GetCantones()
        {
            //var cantones = await new BS.Canton(_context).GetAllIncludeWithAsync();

            var mappAux = _mapper.Map<IEnumerable<DataModels.Cantones>>(
                await new BS.Canton(_context).GetAllIncludeWithAsync()
            ).ToList();
            return mappAux;
        }

        // GET: api/Cantones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Cantones>> GetCantones(int id)
        {
            var cantones = _mapper.Map<DataModels.Cantones>(
                await new BS.Canton(_context).GetOneByIdIncludeWihAsync(id)
            );

            if (cantones == null)
            return NotFound();

            return cantones;
        }

        // PUT: api/Cantones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCantones(int id, DataModels.Cantones cantones)
        {
            if (id != cantones.IdCanton)
            return BadRequest();
            
            try
            {
                new BS.Canton(_context).Update(
                        _mapper.Map<DOObjects.Cantones>(cantones)
                );
            }
            catch (Exception)
            {
                if (!CantonesExists(id))
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
        
        // POST: api/Cantones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Cantones>> PostCantones(DataModels.Cantones cantones)
        {
            try
            {
                new BS.Canton(_context).Insert(
                        _mapper.Map<DOObjects.Cantones>(cantones)
                );
            }
            catch (Exception)
            {
                if (CantonesExists(cantones.IdCanton))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCantones", new { id = cantones.IdCanton }, cantones);
        }
        
        // DELETE: api/Cantones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Cantones>> DeleteCantones(int id)
        {
            var cantones = new BS.Canton(_context).GetOneById(id);
            if (cantones == null)
            return NotFound();
            
            new BS.Canton(_context).Delete(cantones);
        
            return _mapper.Map<DataModels.Cantones>(cantones);
        }
        
        private bool CantonesExists(int id)
        {
            return (new BS.Canton(_context).GetOneById(id) != null);
        }
    }
}
