using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class ListaOferente : ICRUD<data.ListaOferentes>
    {
        private SolutionDbContext _repo = null;

        public ListaOferente(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.ListaOferentes t)
        {
            new DAL.ListaOferente(_repo).Delete(t);
        }

        public IEnumerable<data.ListaOferentes> GetAll()
        {
            return new DAL.ListaOferente(_repo).GetAll();
        }

        public data.ListaOferentes GetOneById(int id)
        {
            return new DAL.ListaOferente(_repo).GetOneById(id);
        }

        public void Insert(data.ListaOferentes t)
        {
            new DAL.ListaOferente(_repo).Insert(t);
        }

        public void Update(data.ListaOferentes t)
        {
            new DAL.ListaOferente(_repo).Update(t);
        }

        public data.ListaOferentes GetOne(Expression<Func<data.ListaOferentes, bool>> t)
        {
            return new DAL.ListaOferente(_repo).GetOne(t);
        }
        
        public async Task<IEnumerable<data.ListaOferentes>> GetAllIncludeWithAsync()
        {
            return await new DAL.ListaOferente(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.ListaOferentes> GetOneByIdsIncludeWihAsync(int idOferente, int idPuesto)
        {
            return await new DAL.ListaOferente(_repo)
                .GetOneByIdsIncludeWihAsync(idOferente, idPuesto);
        }
    }
}