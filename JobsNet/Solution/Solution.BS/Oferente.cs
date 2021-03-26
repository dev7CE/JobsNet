using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Oferente : ICRUD<data.Oferentes>
    {
        private SolutionDbContext _repo = null;

        public Oferente(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.Oferentes t)
        {
            new DAL.Oferentes(_repo).Delete(t);
        }

        public IEnumerable<data.Oferentes> GetAll()
        {
            return new DAL.Oferentes(_repo).GetAll();
        }

        public data.Oferentes GetOneById(int id)
        {
            return new DAL.Oferentes(_repo).GetOneById(id);
        }

        public void Insert(data.Oferentes t)
        {
            new DAL.Oferentes(_repo).Insert(t);
        }

        public void Update(data.Oferentes t)
        {
            new DAL.Oferentes(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Oferentes>> GetAllIncludeWithAsync()
        {
            return await new DAL.Oferentes(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.Oferentes> GetOneByIdIncludeWihAsync(int id)
        {
            return await new DAL.Oferentes(_repo).GetOneByIdIncludeWithAsync(id);
        }
    }
}