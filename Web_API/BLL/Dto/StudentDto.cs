using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class StudentDto
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string FullName => $"{FirstMidName} {LastName}"; 
        public DateTime EnrollmentDate { get; set; }

        //public ICollection<Enrollment> Enrollments { get; set; }
        public string Mail { set; get; }
        public int Age { set; get; }
    }
}
