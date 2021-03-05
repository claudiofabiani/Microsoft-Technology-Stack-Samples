using BLL.Dto;
using DAL.Domain;
using DAL.Extension.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Interface
{
    public interface IStudentManager
    {

        Task<StudentDto> GetStudentByIdAsync(int id);
     
        Task<PaginatedEnumerableDto<StudentDto>> ListAsync(
            int pageIndex, int pageSize,
            string sortBy = null, bool? ascending = null,
            int? id = null, string lastName = null,
            string firstMidName = null, DateTime? enrollmentStartDate = null,
            DateTime? enrollmentEndDate = null, List<int> enrollmentsId = null,
            string mail = null, int? age = null, bool? asNoTracking = null);
    }
}
