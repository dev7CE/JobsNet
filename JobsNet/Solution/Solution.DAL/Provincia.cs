using System.Collections.Generic;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Provincia : ICRUD<data.Provincias>
    {
        private Repository<data.Provincias> _repo = null;

        public Provincia(SolutionDbContext dbContext)
        {
            _repo = new Repository<data.Provincias>(dbContext);
        }

        public void Delete(data.Provincias t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Provincias> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Provincias GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Provincias t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Provincias t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
