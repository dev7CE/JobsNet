using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class PuestosTrabajo : ICRUD<data.PuestosTrabajo>
    {
        private SolutionDbContext _repo = null;

        public PuestosTrabajo(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.PuestosTrabajo t)
        {
            new DAL.PuestoTrabajo(_repo).Delete(t);
        }

        public IEnumerable<data.PuestosTrabajo> GetAll()
        {
            return new DAL.PuestoTrabajo(_repo).GetAll();
        }

        public data.PuestosTrabajo GetOneById(int id)
        {
            return new DAL.PuestoTrabajo(_repo).GetOneById(id);
        }

        public void Insert(data.PuestosTrabajo t)
        {
            new DAL.PuestoTrabajo(_repo).Insert(t);
        }

        public void Update(data.PuestosTrabajo t)
        {
            new DAL.PuestoTrabajo(_repo).Update(t);
        }

        public async Task<IEnumerable<data.PuestosTrabajo>> GetAllIncludeWithAsync()
        {
            return await new DAL.PuestoTrabajo(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.PuestosTrabajo> GetOneByIdIncludeWihAsync(int id)
        {
            return await new DAL.PuestoTrabajo(_repo).GetOneByIdIncludeWithAsync(id);
        }
    }
}