using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using StudentSystem.Data;
using StudentSystem.Data.Migrations;
using StudentSystem.Models;



namespace StudentSystem.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            //set and seed database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());

            var context = new StudentSystemContext();

            var count = context.Students.Count();
            Console.WriteLine(count);


            //working with database

            //1.Lists all students and their homework submissions. Select only their names and for each homework - content and content-type.
           var studentsWithHomeworks = context.Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        h.Content,
                        h.ContentType
                    }).ToList()
                }).ToList();
            foreach (var student in studentsWithHomeworks)
            {
                Console.WriteLine("Student: {0}", student.Name);
                if (student.Homeworks.Count == 0)
                {
                    Console.WriteLine("no homeworks");
                }
                else
                {
                    foreach (var homework in student.Homeworks)
                    {
                        Console.WriteLine("Homeworks: {0} - {1}", homework.Content, homework.ContentType);
                    }
                }
            }
            Console.WriteLine();

            //2.List all courses with their corresponding resources. Select the course name and description and everything for each resource.
            //Order the courses by start date (ascending), then by end date (descending).
            var coursesWithResourses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources.Select(r => new
                    {
                        r.Name,
                        r.ResourceType,
                        r.Url
                    }).ToList()
                }).ToList();
            foreach (var course in coursesWithResourses)
            {
                Console.WriteLine("Course: {0} - Description: {1}", course.Name, course.Description);
                foreach (var resourse in course.Resources)
                {
                    Console.WriteLine("Resourse: {0}; type: {1}; url: {2}", resourse.Name, resourse.ResourceType, resourse.Url);
                }
            }
            Console.WriteLine();

            //3.List all courses with more than 1 resource. Order them by resources count (descending), then by start date (descending).
            //Select only the course name and the resource count.
            var courses = context.Courses
                .Where(c => c.Resources.Count > 1)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    NumberOfResourses = c.Resources.Count
                }).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine("Course: {0} - Number of resourses: {1}", course.Name, course.NumberOfResourses);
            }
            Console.WriteLine();


            //4.List all courses which were active on a given date (choose the date depending on the data seeded to ensure there are results),
            //and for each course count the number of students enrolled. Select the course name, start and end date,
            //course duration (difference between end and start date) and number of students enrolled. 
            //Order the results by the number of students enrolled (in descending order), then by duration (descending).
            var activeCourses = context.Courses
                .Where(c => c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now)
                .OrderByDescending(c => c.Students.Count())
                .ThenByDescending(c => SqlFunctions.DateDiff("DAY", c.StartDate, c.EndDate)) //or EntityFunctions.DiffDays(c.StartDate, c.EndDate)
                .Select(c => new
                             {
                                 c.Name,
                                 c.StartDate,
                                 c.EndDate,
                                 Duration = SqlFunctions.DateDiff("DAY", c.StartDate, c.EndDate),
                                 NumberOfStudents = c.Students.Count()
                             }).ToList();
            foreach (var course in activeCourses)
            {
                Console.WriteLine("Course: {0}; start date: {1}; end date: {2}; duration: {3} days; number of students: {4}",
                                    course.Name, course.StartDate, course.EndDate, course.Duration, course.NumberOfStudents);
            }
            Console.WriteLine();


            //5.For each student, calculate the number of courses she’s enrolled in, the total price of these courses and
            //the average price per course for the student. Select the student name, number of courses, total price and average price.
            //Order the results by total price (descending), then by number of courses (descending)
            //and then by the student’s name (ascending)
            var students = context.Students
                .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                .ThenByDescending(s => s.Courses.Count)
                .ThenBy(s => s.Name)
                .Select(s => new
                {
                    StudentName = s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Price),
                    AveragePricePerCourse = s.Courses.Average(c => c.Price)
                }).ToList();
            foreach (var student in students)
            {
                Console.WriteLine("Sudent: {0}; number of courses: {1}; total price of courses: {2}; average price per course: {3}",
                                        student.StudentName, student.NumberOfCourses, student.TotalPrice, student.AveragePricePerCourse);
            }


            //change database: add table Licenses and populate it

            //create few Licenses
            License ccbyncsa = new License()
            {
                Name = "Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International"
            };


            License telericLicense = new License()
            {
                Name = "HTML Basics course by Telerik Academy under CC-BY-NC-SA license"
            };

            context.Licenses.AddOrUpdate(ccbyncsa, telericLicense);

            //extract Resourses from the database
            var lab = context.Resources.Find(1);
            var lecture = context.Resources.Find(2);

            //add relationships between licenses and resourses
            lab.Licenses = new HashSet<License> { ccbyncsa };
            lecture.Licenses = new HashSet<License> { ccbyncsa, telericLicense };
            
            context.Resources.AddOrUpdate(lab, lecture);

            context.SaveChanges();
        }
    }
}
