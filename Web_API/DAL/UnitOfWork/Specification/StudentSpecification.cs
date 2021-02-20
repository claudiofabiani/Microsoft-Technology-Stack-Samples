using DAL.Domain;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.UnitOfWork.Specification
{
    public class StudentSpecification : BaseSpecification<Student>
    {
        public int? Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime? EnrollmentStartDate { get; set; }
        public DateTime? EnrollmentEndDate { get; set; }

        public List<int> EnrollmentsId { get; set; }
        public string Mail { set; get; }
        public int? Age { set; get; }

        public StudentSpecification()
        {

        }

        public void SetCriteria()
        {
            ParameterExpression mainPredicateParam = Expression.Parameter(typeof(Student));
            if (Id.HasValue)
            {
                Criteria = i => i.ID == Id;
            }
            if (!String.IsNullOrWhiteSpace(LastName))
            {
                Expression nameProperty = Expression.Property(mainPredicateParam, "LastName");
                Expression<Func<Student, bool>> func = Expression.Lambda<Func<Student, bool>>(
                    Expression.Call(
                        MakeString(nameProperty),
                        "Contains",
                        Type.EmptyTypes,
                        Expression.Constant(LastName, typeof(string))
                    ),
                    mainPredicateParam
                );

                Criteria = (Criteria != null) ? Expression.Lambda<Func<Student, bool>>(Expression.And(Criteria.Body, func.Body)) : func;
            }
            if (!String.IsNullOrWhiteSpace(FirstMidName))
            {
                Expression nameProperty = Expression.Property(mainPredicateParam, "FirstMidName");
                Expression<Func<Student, bool>> func = Expression.Lambda<Func<Student, bool>>(
                    Expression.Call(
                        MakeString(nameProperty), 
                        "Contains", 
                        Type.EmptyTypes, 
                        Expression.Constant(FirstMidName, typeof(string))
                    ),
                    mainPredicateParam
                );                
                
                Criteria = (Criteria != null) ? Expression.Lambda<Func<Student, bool>>(Expression.And(Criteria.Body, func.Body)) : func;
            }
            if (EnrollmentStartDate.HasValue)
            {
                var left = Expression.Property(mainPredicateParam, "EnrollmentDate");
                var right = Expression.Constant(EnrollmentStartDate.Value, typeof(DateTime));

                Expression<Func<Student, bool>> func = Expression.Lambda<Func<Student, bool>>(

                    Expression.GreaterThanOrEqual(left, right),
                    mainPredicateParam
                );

                Criteria = (Criteria != null) ? Expression.Lambda<Func<Student, bool>>(Expression.And(Criteria.Body, func.Body), mainPredicateParam) : func;
            }
            if (EnrollmentEndDate.HasValue)
            {
                var left = Expression.Property(mainPredicateParam, "EnrollmentDate");
                var right = Expression.Constant(EnrollmentEndDate.Value, typeof(DateTime));

                Expression<Func<Student, bool>> func = Expression.Lambda<Func<Student, bool>>(

                    Expression.LessThanOrEqual(left, right),
                    mainPredicateParam
                );

                Criteria = (Criteria != null) ? Expression.Lambda<Func<Student, bool>>(Expression.And(Criteria.Body, func.Body), mainPredicateParam) : func;
            }
            if (!String.IsNullOrWhiteSpace(Mail))
            {
                Expression nameProperty = Expression.Property(mainPredicateParam, "Mail");
                Expression<Func<Student, bool>> func = Expression.Lambda<Func<Student, bool>>(
                    Expression.Call(
                        MakeString(nameProperty),
                        "Contains",
                        Type.EmptyTypes,
                        Expression.Constant(Mail, typeof(string))
                    ),
                    mainPredicateParam
                );

                Criteria = (Criteria != null) ? Expression.Lambda<Func<Student, bool>>(Expression.And(Criteria.Body, func.Body)) : func;
            }

        }

        private static Expression MakeString(Expression source)
        {
            return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
        }
    }
}
