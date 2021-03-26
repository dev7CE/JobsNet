using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Oferentes : ICRUD<data.Oferentes>
    {
        private RepositoryOferentes _repo = null;

        public Oferentes(SolutionDbContext dbContext)
        {
            _repo = new RepositoryOferentes(dbContext);
        }

        public void Delete(data.Oferentes t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Oferentes> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Oferentes GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Oferentes t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Oferentes t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Oferentes>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.Oferentes> GetOneByIdIncludeWithAsync(int id)
        {
            return await _repo.GetOneByIdWithAsync(id);
        }
    }
}
