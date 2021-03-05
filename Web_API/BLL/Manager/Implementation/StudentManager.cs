using AutoMapper;
using BLL.Dto;
using BLL.Manager.Interface;
using DAL.Domain;
using DAL.Extension.Domain;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Specification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Implementation
{
    public class StudentManager : IStudentManager
    {
        private readonly ILogger<StudentManager> _logger;
        private readonly IMapper _mapper;

        private UnitOfWork _uow { set; get; }
        public StudentManager(
            ILogger<StudentManager> logger,
            IMapper mapper,
            UnitOfWork uow
            ) 
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            try
            {
                Student student = await _uow.StudentRepository.GetByIDAsync(id);
                StudentDto studentDto = _mapper.Map<StudentDto>(student);
                return studentDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
        public async Task<PaginatedEnumerableDto<StudentDto>> ListAsync(
            int pageIndex, int pageSize, string sortBy = null, bool? ascending = false,
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

                PaginatedEnumerable<Student> students = await _uow.StudentRepository.ListPaginatedAsync(specification);

                StudentDto sa = _mapper.Map<StudentDto>(students.Items.FirstOrDefault());

                IEnumerable<StudentDto> s = _mapper.Map<IEnumerable<StudentDto>>(students.Items);

                PaginatedEnumerableDto<StudentDto> studentsDto = _mapper.Map<PaginatedEnumerableDto<StudentDto>>(students);
                
                return studentsDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

    }
}
