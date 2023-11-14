using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class VacancyVM
    {

        public string Status { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int WorkingHours { get; set; }
        public string JobOffer { get; set; }
        public int MajorName { get; set; }
    }
}
