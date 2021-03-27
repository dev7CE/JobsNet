using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class PuestoTrabajo : ICRUD<data.PuestosTrabajo>
    {
        private RepositoryPuestosTrabajo _repo = null;

        public PuestoTrabajo(SolutionDbContext dbContext)
        {
            _repo = new RepositoryPuestosTrabajo(dbContext);
        }

        public void Delete(data.PuestosTrabajo t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.PuestosTrabajo> GetAll()
        {
            return _repo.GetAll();
        }

        public data.PuestosTrabajo GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.PuestosTrabajo t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.PuestosTrabajo t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.PuestosTrabajo>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.PuestosTrabajo> GetOneByIdIncludeWithAsync(int id)
        {
            return await _repo.GetOneByIdWithAsync(id);
        }
    }
}
