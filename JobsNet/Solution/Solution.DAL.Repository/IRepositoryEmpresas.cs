using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryEmpresas : IRepository<Empresas> 
    {
        Task<IEnumerable<Empresas>> GetAllWithAsync();
        Task<Empresas> GetOneByIdWithAsync(int id);
    }
}
