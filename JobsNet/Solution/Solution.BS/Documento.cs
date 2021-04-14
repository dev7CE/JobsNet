using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Documento : ICRUD<data.Documentos>
    {
        private SolutionDbContext _repo = null;

        public Documento(SolutionDbContext dbContext)
        {
            this._repo = dbContext;
        }

        public void Delete(data.Documentos t)
        {
            new DAL.Documento(_repo).Delete(t);
        }

        public IEnumerable<data.Documentos> GetAll()
        {
            return new DAL.Documento(_repo).GetAll();
        }

        public data.Documentos GetOneById(int id)
        {
            return new DAL.Documento(_repo).GetOneById(id);
        }

        public void Insert(data.Documentos t)
        {
            new DAL.Documento(_repo).Insert(t);
        }

        public void Update(data.Documentos t)
        {
            new DAL.Documento(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Documentos>> GetAllIncludeWithAsync()
        {
            return await new DAL.Documento(_repo).GetAllIncludeWithAsync();
        }

        public async Task<data.Documentos> GetOneByIdIncludeWihAsync(int id)
        {
            return await new DAL.Documento(_repo).GetOneByIdIncludeWithAsync(id);
        }
    }
}
