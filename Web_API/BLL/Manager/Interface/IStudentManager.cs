using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Interface
{
    public interface IStudentManager
    {
        IEnumerable<Student> List(
            int pageIndex, int pageSize,
            int? id = null, string lastName = null,
            string firstMidName = null, DateTime? enrollmentStartDate = null,
            DateTime? enrollmentEndDate = null, List<int> enrollmentsId = null,
            string mail = null, int? age = null, bool? asNoTracking = null);

        Task<IEnumerable<Student>> ListAsync(
            int pageIndex, int pageSize,
            string sortBy = null, bool? ascending = null,
            int? id = null, string lastName = null,
            string firstMidName = null, DateTime? enrollmentStartDate = null,
            DateTime? enrollmentEndDate = null, List<int> enrollmentsId = null,
            string mail = null, int? age = null, bool? asNoTracking = null);
    }
}
