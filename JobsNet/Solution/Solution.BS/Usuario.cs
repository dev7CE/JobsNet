using System.Collections.Generic;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Usuario : ICRUD<data.Usuarios>
    {
        private SolutionDbContext _dbContext = null;

        public Usuario(SolutionDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Delete(Usuarios t)
        {
            new DAL.Usuario(_dbContext).Delete(t);
        }

        public IEnumerable<Usuarios> GetAll()
        {
            return new DAL.Usuario(_dbContext).GetAll();
        }

        public Usuarios GetOneById(int id)
        { return new DAL.Usuario(_dbContext).GetOneById(id); }
        
        public Usuarios GetOneByUserName(string userName)
        { return new DAL.Usuario(_dbContext).GetOneByUserName(userName); }

        public void Insert(Usuarios t)
        {
            new DAL.Usuario(_dbContext).Insert(t);
        }

        public void Update(Usuarios t)
        {
            new DAL.Usuario(_dbContext).Update(t);
        }
    }
}