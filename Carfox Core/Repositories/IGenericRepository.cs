using Carfox.Core.Entities;
using Carfox.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Static Queries
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id); 
        #endregion

        #region Dynamic Quries Using SpecificationDesignPattern
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        #endregion

        Task AddProductAsync(T Entity);

        void UpdateProduct(T Entity);

        void DeleteProduct(T Entity);

        public Task UpdateDatabase();

        Task<IReadOnlyList<T>> GetAllByIdWithSpecAsync(ISpecification<T> spec);




    }
}
