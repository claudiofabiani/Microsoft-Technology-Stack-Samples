using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.UnitOfWork.Specification
{
    // https://github.com/dotnet-architecture/eShopOnWeb
    public interface ISpecification<T>
    {
        bool AsNoTracking { get; }
        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
