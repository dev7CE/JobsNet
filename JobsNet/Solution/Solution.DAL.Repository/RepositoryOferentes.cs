using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public class RepositoryOferentes : Repository<Oferentes>, IRepositoryOferentes
    {
        public RepositoryOferentes(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<Oferentes>> GetAllWithAsync()
        {
            return await _context.Oferentes
                .Include(e => e.Usuario)
                .ToListAsync();
        }

        public async Task<Oferentes> GetOneByIdWithAsync(int id)
        {
            return await _context.Oferentes
                .Include(e => e.Usuario)
                .SingleOrDefaultAsync(e => e.IdOferente == id);
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
