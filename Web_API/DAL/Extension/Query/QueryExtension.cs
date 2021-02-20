using DAL.Extension.Domain;
using DAL.UnitOfWork.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extension.Query
{
    public static class QueryExtension
    {
        public static async Task<PaginatedEnumerable<T>> ToPaginatedAsync<T>(this IQueryable<T> source, ISpecification<T> specification)
        {
            try
            {
                if (specification == null || specification.Take == 0)
                {
                    throw new ArgumentException();
                }
                var t2 = source.Count();

                // inverti skip = (pageIndex - 1) * pageSize per il page index  1+ (skip/pagesize)

                source = (IQueryable<T>)source.Skip(specification.Skip).Take(specification.Take);

                var t1 = await source.ToListAsync();
                return t1.ToPaginated(t2, 1 + (specification.Skip / specification.Take), specification.Take > 0 ? specification.Take : t2);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
