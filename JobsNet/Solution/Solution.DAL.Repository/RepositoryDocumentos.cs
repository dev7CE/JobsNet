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
    public class RepositoryDocumentos : Repository<Documentos>, IRepositoryDocumentos
    {
        public RepositoryDocumentos(SolutionDbContext solutionDbContext)
            : base(solutionDbContext) { }

        public async Task<IEnumerable<Documentos>> GetAllWithAsync()
        {
            return await _context.Documentos
                .Include(d => d.Usuario).ToListAsync();
        }

        public async Task<Documentos> GetOneByIdWithAsync(int id)
        {
            return await _context.Documentos
                .Include(d => d.Usuario)
                .SingleOrDefaultAsync(d => d.Id == id);
        }

        private SolutionDbContext _context {
            get { return _dbContext as SolutionDbContext; }
        }
    }
}
