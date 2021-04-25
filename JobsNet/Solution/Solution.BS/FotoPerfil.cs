using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class FotoPerfil : ICRUD<data.FotosPerfil>
    {
        private SolutionDbContext _repo = null;

        public FotoPerfil(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.FotosPerfil t)
        {
            new DAL.FotoPerfil(_repo).Delete(t);
        }

        public IEnumerable<data.FotosPerfil> GetAll()
        {
            return new DAL.FotoPerfil(_repo).GetAll();
        }

        public data.FotosPerfil GetOneById(int id)
        {
            return new DAL.FotoPerfil(_repo).GetOneById(id);
        }

        public void Insert(data.FotosPerfil t)
        {
            new DAL.FotoPerfil(_repo).Insert(t);
        }

        public void Update(data.FotosPerfil t)
        {
            new DAL.FotoPerfil(_repo).Update(t);
        }

        public async Task<IEnumerable<data.FotosPerfil>> GetAllIncludeWithAsync()
        {
            return await new DAL.FotoPerfil(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.FotosPerfil> GetOneByIdIncludeWihAsync(int id)
        {
            return await new DAL.FotoPerfil(_repo).GetOneByIdIncludeWithAsync(id);
        }
    }
}
