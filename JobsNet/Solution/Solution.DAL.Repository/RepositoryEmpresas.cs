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
    public class RepositoryEmpresas : Repository<Empresas>, IRepositoryEmpresas
    {
        public RepositoryEmpresas(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<Empresas>> GetAllWithAsync()
        {
            return await _context.Empresas
                .Include(e => e.Canton)
                    .ThenInclude(c => c.Provincia)
                .Include(e => e.Usuario)
                .ToListAsync();
        }

        public async Task<Empresas> GetOneByIdWithAsync(int id)
        {
            return await _context.Empresas
                .Include(e => e.Canton)
                    .ThenInclude(c => c.Provincia)
                .Include(e => e.Usuario)
                .SingleOrDefaultAsync(e => e.IdEmpresa == id);
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
