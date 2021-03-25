using System.Collections.Generic;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Usuario : ICRUD<data.Usuarios>
    {
        private Repository<data.Usuarios> _repo = null;

        public Usuario(SolutionDbContext dbContext)
        {
            _repo = new Repository<data.Usuarios>(dbContext);
        }

        public void Delete(data.Usuarios t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Usuarios> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Usuarios GetOneById(int id)
        { return null; }
        public data.Usuarios GetOneByUserName(string userName)
        { return _repo.GetOne(e => e.UserName == userName); }

        public void Insert(data.Usuarios t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Usuarios t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
