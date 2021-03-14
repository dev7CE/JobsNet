using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryCantones : IRepository<Cantones> 
    {
        Task<IEnumerable<Cantones>> GetAllWithAsync();
        Task<Cantones> GetOneByIdWithAsync(int id);
    }
}
