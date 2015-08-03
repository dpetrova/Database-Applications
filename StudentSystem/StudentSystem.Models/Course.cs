using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Resource> resources;
        private ICollection<Homework> homeworks;

        public Course()
        {
            this.students = new HashSet<Student>();
            this.resources = new HashSet<Resource>();
            this.homeworks = new HashSet<Homework>();
        }
            
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Course's name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Course start date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Course end date is required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Course price is required")]
        public decimal Price { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
