using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryDocumentos : IRepository<Documentos> 
    {
        Task<IEnumerable<Documentos>> GetAllWithAsync();
        Task<Documentos> GetOneByIdWithAsync(int id);
    }
}
