using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class ListaOferente : ICRUD<data.ListaOferentes>
    {
        private RepositoryListaOferentes _repo = null;

        public ListaOferente(SolutionDbContext dbContext)
        {
            _repo = new RepositoryListaOferentes(dbContext);
        }

        public void Delete(data.ListaOferentes t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.ListaOferentes> GetAll()
        {
            return _repo.GetAll();
        }

        public data.ListaOferentes GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.ListaOferentes t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.ListaOferentes t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public data.ListaOferentes GetOne(Expression<Func<data.ListaOferentes, bool>> t)
        {
            return _repo.GetOne(t);
        }
        
        public async Task<IEnumerable<data.ListaOferentes>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.ListaOferentes> GetOneByIdsIncludeWihAsync(int idOferente, int idPuesto)
        {
            return await _repo.GetOneByIdsIncludeWihAsync(idOferente, idPuesto);
        }
    }
}
