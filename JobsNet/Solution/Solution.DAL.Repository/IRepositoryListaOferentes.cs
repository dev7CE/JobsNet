using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryListaOferentes : IRepository<ListaOferentes> 
    {
        Task<IEnumerable<ListaOferentes>> GetAllWithAsync();
        Task<ListaOferentes> GetOneByIdsIncludeWihAsync(int idOferente, int idPuesto);
    }
}
