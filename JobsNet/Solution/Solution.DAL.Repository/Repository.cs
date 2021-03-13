using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Solution.DAL.EF;
using EFEntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Solution.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SolutionDbContext _dbContext;

        public Repository(SolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Delete(T t)
        {
            _dbContext.Set<T>().Remove(t);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public T GetOne(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).FirstOrDefault();
        }

        public T GetOneById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Insert(T t)
        {
            if (_dbContext.Entry<T>(t).State == EFEntityState.Detached)
            {
                _dbContext.Entry<T>(t).State = EFEntityState.Added;
            }
            else
            {
                _dbContext.Set<T>().Add(t);
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public void Update(T t)
        {
            if (_dbContext.Entry<T>(t).State == EFEntityState.Detached)
            {
                _dbContext.Set<T>().Attach(t);
            }
            _dbContext.Entry<T>(t).State = EFEntityState.Modified;
        }
    }
}
