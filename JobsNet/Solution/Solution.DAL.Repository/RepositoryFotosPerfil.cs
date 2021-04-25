using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;
using EFEntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Solution.DAL.Repository
{
    public class RepositoryFotosPerfil : Repository<FotosPerfil>, IRepositoryFotosPerfil
    {
        public RepositoryFotosPerfil(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<FotosPerfil>> GetAllWithAsync()
        {
            return await _context.FotosPerfil
                .Include(d => d.Usuario).ToListAsync();
        }

        public async Task<FotosPerfil> GetOneByIdWithAsync(int id)
        {
            return await _context.FotosPerfil
                .Include(d => d.Usuario)
                .SingleOrDefaultAsync(d => d.Id == id);
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
