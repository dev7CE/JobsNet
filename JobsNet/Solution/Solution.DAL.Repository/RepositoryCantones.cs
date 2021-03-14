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
    public class RepositoryCantones : Repository<Cantones>, IRepositoryCantones
    {
        public RepositoryCantones(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<Cantones>> GetAllWithAsync()
        {
            return await _context.Cantones
                .Include(c => c.Provincia).ToListAsync();
        }

        public async Task<Cantones> GetOneByIdWithAsync(int id)
        {
            return await _context.Cantones
                .Include(p => p.Provincia)
                .SingleOrDefaultAsync(p => p.IdCanton == id);
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
