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
    public class RepositoryListaOferentes : Repository<ListaOferentes>, IRepositoryListaOferentes
    {
        public RepositoryListaOferentes(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<ListaOferentes>> GetAllWithAsync()
        {
            return await _context.ListaOferentes
                .Include(l => l.PuestoTrabajo)
                .Include(l => l.Oferente)
                .ToListAsync();
        }

        public async Task<ListaOferentes> GetOneByIdsIncludeWihAsync(int idOferente, int idPuesto)
        {
            return await _context.ListaOferentes
                .Include(l => l.PuestoTrabajo)
                .Include(l => l.Oferente)
                .SingleOrDefaultAsync(l => 
                    (l.IdOferente == idOferente &&
                    l.IdPuesto == idPuesto));
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
