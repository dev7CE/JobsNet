using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryPuestosTrabajo : IRepository<PuestosTrabajo> 
    {
        Task<IEnumerable<PuestosTrabajo>> GetAllWithAsync();
        Task<PuestosTrabajo> GetOneByIdWithAsync(int id);
    }
}
