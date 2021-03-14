using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Canton : ICRUD<data.Cantones>
    {
        private SolutionDbContext _repo = null;

        public Canton(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.Cantones t)
        {
            new DAL.Canton(_repo).Delete(t);
        }

        public IEnumerable<data.Cantones> GetAll()
        {
            return new DAL.Canton(_repo).GetAll();
        }

        public data.Cantones GetOneById(int id)
        {
            return new DAL.Canton(_repo).GetOneById(id);
        }

        public void Insert(data.Cantones t)
        {
            new DAL.Canton(_repo).Insert(t);
        }

        public void Update(data.Cantones t)
        {
            new DAL.Canton(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Cantones>> GetAllIncludeWithAsync()
        {
            return await new DAL.Canton(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.Cantones> GetOneByIdIncludeWihAsync(int id)
        {
            return await new DAL.Canton(_repo).GetOneByIdIncludeWithAsync(id);
        }
    }
}