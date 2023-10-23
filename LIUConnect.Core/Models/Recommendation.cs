using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Recommendation
    {
        [Key]
        public int RecommnedationId { get; set; }
        [Required]
        public int InstructorID { get; set; }
        [Required]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Instructor Instructor { get; set; }  
        public Student Student { get; set; }
    }
}
