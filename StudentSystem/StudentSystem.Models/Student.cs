using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace StudentSystem.Models
{
    public class Student
    {
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks;

        public Student()
        {
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();
        }
        
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Student's name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Registration date is required")]
        public DateTime RegisteredOn { get; set; }
        public DateTime? BirthDay { get; set; }

        public virtual ICollection<Course> Courses
        { 
            get { return this.courses; }
            set { this.courses = value; } 
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
