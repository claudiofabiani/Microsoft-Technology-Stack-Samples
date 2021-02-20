using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Manager.Interface
{
    public interface IStudentManager
    {
        IEnumerable<Student> List(
            int? id = null, string lastName = null,
            string firstMidName = null, DateTime? enrollmentStartDate = null,
            DateTime? enrollmentEndDate = null, List<int> enrollmentsId = null,
            string mail = null, int? age = null, bool? asNoTracking = null);
    }
}
