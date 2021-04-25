using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class FotoPerfil : ICRUD<data.FotosPerfil>
    {
        private RepositoryFotosPerfil _repo = null;

        public FotoPerfil(SolutionDbContext dbContext)
        {
            _repo = new RepositoryFotosPerfil(dbContext);
        }

        public void Delete(data.FotosPerfil t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.FotosPerfil> GetAll()
        {
            return _repo.GetAll();
        }

        public data.FotosPerfil GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.FotosPerfil t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.FotosPerfil t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.FotosPerfil>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.FotosPerfil> GetOneByIdIncludeWithAsync(int id)
        {
            return await _repo.GetOneByIdWithAsync(id);
        }
    }
}
