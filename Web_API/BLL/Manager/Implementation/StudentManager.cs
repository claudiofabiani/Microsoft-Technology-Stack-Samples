using BLL.Manager.Interface;
using DAL.Domain;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Specification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Implementation
{
    public class StudentManager : IStudentManager
    {
        private readonly ILogger<StudentManager> _logger;

        private UnitOfWork _uow { set; get; }
        public StudentManager(
            ILogger<StudentManager> logger,
            UnitOfWork uow
            ) 
        {
            _logger = logger;
            _uow = uow;
        }

        public IEnumerable<Student> List(
            int pageIndex, int pageSize,
            int? id = null, string lastName = null, 
            string firstMidName = null, DateTime? enrollmentStartDate = null, 
            DateTime? enrollmentEndDate = null, List<int> enrollmentsId = null,
            string mail = null, int? age = null, bool? asNoTracking = null)
        {
            try
            {
                StudentSpecification specification = new StudentSpecification()
                {
                    Id = id,
                    LastName = lastName,
                    FirstMidName = firstMidName,
                    EnrollmentStartDate = enrollmentStartDate,
                    EnrollmentEndDate = enrollmentEndDate,
                    EnrollmentsId = enrollmentsId,
                    Mail = mail,
                    Age = age,
                };
                specification.SetCriteria();
                if (asNoTracking.HasValue && asNoTracking.Value)
                {
                    specification.ApplyNoTracking();
                }
                specification.SetPagination(pageIndex, pageSize);
                

                IEnumerable<Student> students = _uow.StudentRepository.List(specification);

                return students;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Student>> ListAsync(
            int pageIndex, int pageSize,
            string sortBy = null, bool? ascending = false,
            int? id = null, string lastName = null,
            string firstMidName = null, DateTime? enrollmentStartDate = null,
            DateTime? enrollmentEndDate = null, List<int> enrollmentsId = null,
            string mail = null, int? age = null, bool? asNoTracking = null)
        {
            try
            {
                StudentSpecification specification = new StudentSpecification(pageIndex, pageSize, asNoTracking, sortBy, ascending)
                {
                    Id = id,
                    LastName = lastName,
                    FirstMidName = firstMidName,
                    EnrollmentStartDate = enrollmentStartDate,
                    EnrollmentEndDate = enrollmentEndDate,
                    EnrollmentsId = enrollmentsId,
                    Mail = mail,
                    Age = age,
                };
                //specification.SetCriteria();
                //if (asNoTracking.HasValue && asNoTracking.Value)
                //{
                //    specification.ApplyNoTracking();
                //}
                //specification.SetPagination(pageIndex, pageSize);

                var s = await _uow.StudentRepository.ListPaginatedAsync(specification);
                IEnumerable<Student> students = await _uow.StudentRepository.ListAsync(specification);

                // manca la mappatura nel modello dto
                // oltre agli oggetti ritornati serve il totale così che il front end possa creare la paginazione

                return students;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
