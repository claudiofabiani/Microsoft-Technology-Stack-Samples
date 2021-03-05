using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DAL.UnitOfWork.Specification
{
    //https://medium.com/@rudyzio92/net-core-using-the-specification-pattern-alongside-a-generic-repository-318cd4eea4aa
    // https://github.com/dotnet-architecture/eShopOnWeb
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecification()
        {
        }

        public BaseSpecification(int pageIndex = 0, int pageSize = 0, bool? asNoTracking = false, string sortBy = null, bool? ascending = false)
        {
            if (ascending.HasValue && !String.IsNullOrWhiteSpace(sortBy))
            {
                ApplySorting(sortBy, ascending.Value);
            }
            if (asNoTracking.HasValue && asNoTracking.Value)
            {
                ApplyNoTracking();
            }
            SetPagination(pageIndex, pageSize); // va messo fuori dallo specification, va nella gestione della risposta paginata
        }

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        public bool AsNoTracking { get; private set; }

        //da verificare il funzionamento degli include
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        public void SetPagination(int pageIndex, int pageSize)
        {
            if (pageIndex != 0 && pageSize != 0)
            {
                int skip = (pageIndex - 1) * pageSize;
                int take = pageSize;
                this.ApplyPaging(skip, take);
            }
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }

        public virtual void ApplyNoTracking()
        {
            AsNoTracking = true;
        }

        // https://stackoverflow.com/questions/63082758/ef-core-specification-pattern-add-all-column-for-sorting-data-with-custom-specif
        protected void ApplySorting(string sort, bool ascending)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                var descending = !ascending;
                var propertyName = sort;

                var specificationType = GetType().BaseType;
                var targetType = specificationType.GenericTypeArguments[0];
                var property = targetType.GetRuntimeProperty(propertyName) ??
                               throw new InvalidOperationException($"Because the property {propertyName} does not exist it cannot be sorted.");

                // Create an Expression<Func<T, object>>.
                var lambdaParamX = Expression.Parameter(targetType, "x");

                var propertyReturningExpression = Expression.Lambda(
                    Expression.Convert(
                        Expression.Property(lambdaParamX, property),
                        typeof(object)),
                    lambdaParamX);

                if (descending)
                {
                    specificationType.GetMethod(
                            nameof(ApplyOrderByDescending),
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Invoke(this, new object[] { propertyReturningExpression });
                }
                else
                {
                    specificationType.GetMethod(
                            nameof(ApplyOrderBy),
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Invoke(this, new object[] { propertyReturningExpression });
                }
            }
        }
    }
}
