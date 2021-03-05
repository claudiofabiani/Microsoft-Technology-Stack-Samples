using DAL.Extension.Domain;
using DAL.Extension.Query;
using DAL.UnitOfWork.Context;
using DAL.UnitOfWork.Repository.Interface;
using DAL.UnitOfWork.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Repository.Implementation
{

    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity: class
    {
        internal EFContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(EFContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> List(ISpecification<TEntity> specification = null)
        {
            IEnumerable<TEntity> list = ApplySpecification(specification).ToList();
            return list;
        }

        public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = ApplySpecification(specification);
            IEnumerable<TEntity> list = await query.ToListAsync();
            return list;
        }

        public async Task<PaginatedEnumerable<TEntity>> ListPaginatedAsync(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = ApplySpecification(specification);
            PaginatedEnumerable<TEntity> list = await query.ToPaginatedAsync(specification);

            return list;
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public int Count(ISpecification<TEntity> specification = null)
        {
            int count = ApplySpecification(specification).Count();
            return count;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(dbSet.AsQueryable(), spec);
        }
    }
}
