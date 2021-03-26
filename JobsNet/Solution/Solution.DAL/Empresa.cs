using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Empresa : ICRUD<data.Empresas>
    {
        private RepositoryEmpresas _repo = null;

        public Empresa(SolutionDbContext dbContext)
        {
            _repo = new RepositoryEmpresas(dbContext);
        }

        public void Delete(data.Empresas t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Empresas> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Empresas GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Empresas t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Empresas t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Empresas>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.Empresas> GetOneByIdIncludeWithAsync(int id)
        {
            return await _repo.GetOneByIdWithAsync(id);
        }
    }
}
