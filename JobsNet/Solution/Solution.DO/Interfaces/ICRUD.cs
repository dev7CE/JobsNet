using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.DO.Interfaces
{
    public interface ICRUD<T>
    {
        void Insert(T t);
        IEnumerable<T> GetAll();
        T GetOneById(int id);
        void Update(T t);
        void Delete(T t);

    }
}
