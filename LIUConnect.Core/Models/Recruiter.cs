using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Recruiter
    {
        public int RecruiterID { get; set; }
        public int UserID { get;set; }
        public User User { get; set; }
        public List<Vacancy> Vacancies { get; set; }

    }
}
