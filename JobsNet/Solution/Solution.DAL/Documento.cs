using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Documento : ICRUD<data.Documentos>
    {
        private RepositoryDocumentos _repo = null;

        public Documento(SolutionDbContext dbContext)
        {
            _repo = new RepositoryDocumentos(dbContext);
        }

        public void Delete(data.Documentos t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Documentos> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Documentos GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Documentos t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Documentos t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Documentos>> GetAllIncludeWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<data.Documentos> GetOneByIdIncludeWithAsync(int id)
        {
            return await _repo.GetOneByIdWithAsync(id);
        }
    }
}
