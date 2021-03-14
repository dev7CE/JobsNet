using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Canton : ICRUD<data.Cantones>
    {
        private RepositoryCantones _repo = null;

        public Canton(SolutionDbContext dbContext)
        {
            _repo = new RepositoryCantones(dbContext);
        }

        public void Delete(data.Cantones t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Cantones> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Cantones GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Cantones t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Cantones t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Cantones>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.Cantones> GetOneByIdIncludeWithAsync(int id)
        {
            return await _repo.GetOneByIdWithAsync(id);
        }
    }
}
