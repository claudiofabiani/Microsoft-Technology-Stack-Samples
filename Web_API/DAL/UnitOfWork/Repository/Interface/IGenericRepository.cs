using DAL.Extension.Domain;
using DAL.UnitOfWork.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> List(ISpecification<TEntity> spec);
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification = null);
        Task<PaginatedEnumerable<TEntity>> ListPaginatedAsync(ISpecification<TEntity> specification = null);

        TEntity GetByID(object id);
        Task<TEntity> GetByIDAsync(object id);

        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        int Count(ISpecification<TEntity> specification = null);

        void Delete(object id);

        void Delete(TEntity entityToDelete);
        void DeleteRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);
    }
}
