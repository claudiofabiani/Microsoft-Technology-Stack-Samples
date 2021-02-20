using BLL.Manager.Interface;
using DAL.Domain;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Specification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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
                

                IEnumerable<Student> students = _uow.StudentRepository.List(specification);

                return students;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
