using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryFotosPerfil : IRepository<FotosPerfil> 
    {
        Task<IEnumerable<FotosPerfil>> GetAllWithAsync();
        Task<FotosPerfil> GetOneByIdWithAsync(int id);
    }
}
