using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Application
    {
        public int ApplicationId { get;set; }
        [ForeignKey("Vacancy")]
        public int VacancyID { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public string status { get; set; }
        public DateTime Datetime { get; set; }  
        public Vacancy Vacancy { get;set; }
        public Student Student { get; set; }


    }
}
