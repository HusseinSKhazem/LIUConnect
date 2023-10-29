using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Vacancy
    {
        public int VacancyId { get; set; }  
        public int RecruiterID {  get; set; }   
        public string Status { get; set; }
        public string Description { get; set; }
        public string Requirements { get;set; }
        public int WorkingHours { get; set; }
        public string JobOffer { get; set; }
        public Recruiter Recruiter { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Application>? Applications { get; set; }
        public int MajorID { get; set; }
        public Major Major { get; set; }


    }
}
