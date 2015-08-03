using StudentSystem.Models;

namespace StudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    

    public class StudentSystemContext : DbContext
    {
        
        public StudentSystemContext()
            : base("StudentSystemContext")
        {
        }

        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Homework> Homeworks { get; set; }
        public IDbSet<Resource> Resources { get; set; }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<License> Licenses { get; set; }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }

}