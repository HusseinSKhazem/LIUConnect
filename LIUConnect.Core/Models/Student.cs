using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public int UserID { get; set; }
        public byte[] CVFileContent { get; set; }
        public User User { get; set; }
        public List<Recommendation> Recommendations { get; }
        public int MajorID { get; set; }
        public Major Major { get; set; }

    }
}
