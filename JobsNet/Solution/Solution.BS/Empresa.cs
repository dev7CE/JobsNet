using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Empresa : ICRUD<data.Empresas>
    {
        private SolutionDbContext _repo = null;

        public Empresa(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.Empresas t)
        {
            new DAL.Empresa(_repo).Delete(t);
        }

        public IEnumerable<data.Empresas> GetAll()
        {
            return new DAL.Empresa(_repo).GetAll();
        }

        public data.Empresas GetOneById(int id)
        {
            return new DAL.Empresa(_repo).GetOneById(id);
        }

        public void Insert(data.Empresas t)
        {
            new DAL.Empresa(_repo).Insert(t);
        }

        public void Update(data.Empresas t)
        {
            new DAL.Empresa(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Empresas>> GetAllIncludeWithAsync()
        {
            return await new DAL.Empresa(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.Empresas> GetOneByIdIncludeWihAsync(int id)
        {
            return await new DAL.Empresa(_repo).GetOneByIdIncludeWithAsync(id);
        }
    }
}