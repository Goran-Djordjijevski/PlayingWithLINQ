using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqQueries
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }

        public int StandardId { get; set; }
    }

    public class Standard
    {
        public int StandardId { get; set; }
        public string StandardName { get; set; }
    }
}
