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
    public class RepositoryPuestosTrabajo : Repository<PuestosTrabajo>, IRepositoryPuestosTrabajo
    {
        public RepositoryPuestosTrabajo(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<PuestosTrabajo>> GetAllWithAsync()
        {
            return await _context.PuestosTrabajo
                .Include(e => e.Empresa)
                .ToListAsync();
        }

        public async Task<PuestosTrabajo> GetOneByIdWithAsync(int id)
        {
            return await _context.PuestosTrabajo
                .Include(e => e.Empresa)
                .SingleOrDefaultAsync(e => e.IdPuesto == id);
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
