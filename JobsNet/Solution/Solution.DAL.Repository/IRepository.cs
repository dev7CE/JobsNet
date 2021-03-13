using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Solution.DAL.Repository
{
    public interface IRepository<T> where T : class 
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
        T GetOne (Expression<Func<T, bool>> predicate);
        T GetOneById(int id);
        void Insert(T t);
        void Update(T t);
        void Delete(T t);
        void Commit();
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);   
    }
}
