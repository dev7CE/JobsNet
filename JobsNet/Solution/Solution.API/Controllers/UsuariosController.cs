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

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Usuarios>>> GetUsuarios()
        {
            return _mapper.Map<IEnumerable<DataModels.Usuarios>>(
                new BS.Usuario(_context).GetAll()
            ).ToList();
        }
        //
        // GET: api/Usuarios/5
        [HttpGet("{userName}")]
        public async Task<ActionResult<DataModels.Usuarios>> GetUsuarios(string userName)
        {
            var usuarios = new BS.Usuario(_context).GetOneByUserName(userName);
            if (usuarios == null)
            return NotFound();

            return _mapper.Map<DataModels.Usuarios>(usuarios);
        }
        //
        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{userName}")]
        public async Task<IActionResult> PutUsuarios(string userName, DataModels.Usuarios usuarios)
        {
            try
            {
                new BS.Usuario(_context).Update(
                    _mapper.Map<data.Usuarios>(usuarios)
                );
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(userName))
                { return NotFound(); }
                else
                { throw; }
            }
            return NoContent();
        }
        //
        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Usuarios>> PostUsuarios(DataModels.Usuarios usuarios)
        {
            try
            {
                new BS.Usuario(_context).Insert(_mapper.Map<data.Usuarios>(usuarios));
            }
            catch (Exception)
            {
                if (UsuariosExists(usuarios.UserName))
                { return Conflict(); }
                else
                { throw; }
            }
            return CreatedAtAction(nameof(GetUsuarios), new { userName = usuarios.UserName }, usuarios);
        }
        //
        // DELETE: api/Usuarios/5
        [HttpDelete("{userName}")]
        public async Task<ActionResult<DataModels.Usuarios>> DeleteUsuarios(string userName)
        {
            var usuario = new BS.Usuario(_context).GetOneByUserName(userName);

            if (usuario == null)
            return NotFound();
            
            new BS.Usuario(_context).Delete(usuario);

            return _mapper.Map<DataModels.Usuarios>(usuario);
        }

        private bool UsuariosExists(string userName)
        {
            //return _context.Usuarios.Any(e => e.UserName == id);
            return (new BS.Usuario(_context).GetOneByUserName(userName) != null);
        }
    }
}
