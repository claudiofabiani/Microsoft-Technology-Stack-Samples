using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class StudentListDto
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Mail { set; get; }
    }
}
