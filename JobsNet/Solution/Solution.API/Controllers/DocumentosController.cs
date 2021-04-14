using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DOObjects = Solution.DO.Objects;
using Solution.DAL.EF;

namespace Solution.API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public DocumentosController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Documentos>>> GetDocumentos()
        {
            return _mapper.Map<IEnumerable<DataModels.Documentos>>(
                await new BS.Documento(_context).GetAllIncludeWithAsync()
                ).ToList();
        }

        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Documentos>> GetDocumentos(int id)
        {
            var documentos = _mapper.Map<DataModels.Documentos>(
                await new BS.Documento(_context).GetOneByIdIncludeWihAsync(id)
            );

            if (documentos == null)
            return NotFound();

            return documentos;
        }
        //
        // PUT: api/Documentos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentos(int id, DataModels.Documentos documentos)
        {
            if (id != documentos.Id)
            return BadRequest();
            
            try
            {
                new BS.Documento(_context).Update(
                    _mapper.Map<DOObjects.Documentos>(documentos)
                );
            }
            catch (Exception)
            {
                if (!DocumentosExists(id))
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
        // POST: api/Documentos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Documentos>> PostDocumentos(DataModels.Documentos documentos)
        {
            try
            {
                new BS.Documento(_context).Insert(
                    _mapper.Map<DOObjects.Documentos>(documentos)
                );
            }
            catch (Exception)
            {
                if (DocumentosExists(documentos.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetDocumentos", new { id = documentos.Id }, documentos);
        }
        //
        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Documentos>> DeleteDocumentos(int id)
        {
            var documentos = new BS.Documento(_context).GetOneById(id);
            if (documentos == null)
            return NotFound();
            
            new BS.Documento(_context).Delete(documentos);

            return _mapper.Map<DataModels.Documentos>(documentos);
        }
        //
        // Exist?
        private bool DocumentosExists(int id)
        {
            return (new BS.Documento(_context).GetOneById(id) != null);
        }
    }
}
