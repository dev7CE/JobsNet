using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryOferentes : IRepository<Oferentes> 
    {
        Task<IEnumerable<Oferentes>> GetAllWithAsync();
        Task<Oferentes> GetOneByIdWithAsync(int id);
    }
}
