using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Recruiter
    {
        public int RecruiterID { get; set; }
        public int UserID { get;set; }
        public bool? isApproved { get; set; }

        public string location { get; set; }

        public string  website { get; set; }

        public User User { get; set; }

        [JsonIgnore]
        public List<Vacancy> Vacancies { get; set; }
        public string? CompanyName {  get; set; }    

        public string? officialFiles { get; set; }

    }
}
