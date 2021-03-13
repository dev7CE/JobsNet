using System.Collections.Generic;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DO.Objects;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Provincia : ICRUD<data.Provincias>
    {
        private SolutionDbContext _dbContext = null;

        public Provincia(SolutionDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Delete(Provincias t)
        {
            new DAL.Provincia(_dbContext).Delete(t);
        }

        public IEnumerable<Provincias> GetAll()
        {
            return new DAL.Provincia(_dbContext).GetAll();
        }

        public Provincias GetOneById(int id)
        {
            return new DAL.Provincia(_dbContext).GetOneById(id);
        }

        public void Insert(Provincias t)
        {
            new DAL.Provincia(_dbContext).Insert(t);
        }

        public void Update(Provincias t)
        {
            new DAL.Provincia(_dbContext).Update(t);
        }
    }
}