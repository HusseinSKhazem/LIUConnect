using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Referral
    { 
        public int ReferralId { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public string ReferralDescription { get; set; }
    }
}
