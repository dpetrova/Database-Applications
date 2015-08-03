using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public class Homework
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Homework's content is required")]
        [MaxLength(300)]
        public string Content { get; set; }
        [Required(ErrorMessage = "Type of homework's content is required")]
        public TypeOfHomework ContentType { get; set; }
        [Required(ErrorMessage = "Submission fate of homework is required")]
        public DateTime SubmissionDate { get; set; }
    }
}
