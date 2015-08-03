using System.Collections.Generic;
using StudentSystem.Models;

namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystemContext context)
        {
            //check if database is empty and run the Seed() method only database is created for the first time
            if (context.Students.Any())
            {
                return;
            }

            //fill few students
            Student petar = new Student()
            {
                Name = "Petar Petrov",
                PhoneNumber = "+359888999444",
                RegisteredOn = new DateTime(2015, 7, 20),
                BirthDay = new DateTime(1995, 6, 29)
            };

            Student mincho = new Student()
            {
                Name = "Mincho Minchev",
                RegisteredOn = new DateTime(2015, 1, 22),
                BirthDay = new DateTime(1999, 2, 15)
            };

            Student mitio = new Student()
            {
                Name = "Dimitar Dimitrov",
                RegisteredOn = DateTime.Now,
                BirthDay = new DateTime(1975, 10, 12)
            };

            //fill few courses
            Course cSharp = new Course()
            {
                Name = "CSharp Advanced",
                Description = @"The Advanced C# course covers methods, arrays, lists, matrices, strings and 
                                regular expressions, functional programming and asynchronous programming.",
                StartDate = new DateTime(2015, 7, 20),
                EndDate = new DateTime(2015, 9, 15),
                Price = 300.00m
            };

            Course databaseApps = new Course()
            {
                Name = "Database Applications",
                StartDate = new DateTime(2016, 2, 19),
                EndDate = new DateTime(2016, 4, 30),
                Price = 250.00m
            };

            Course htmlAndCss = new Course()
            {
                Name = "HTML&CSS",
                StartDate = new DateTime(2016, 5, 15),
                EndDate = new DateTime(2016, 7, 15),
                Price = 300.00m
            };

            //fill few homeworks
            Homework petarHomework = new Homework()
            {
                Content = "Homework: Entity Framework Code First",
                ContentType = TypeOfHomework.Zip,
                SubmissionDate = new DateTime(2015, 7, 20)
            };

            Homework minchoHomework = new Homework()
            {
                Content = "Homework: Responsive Design. Bootstrap",
                ContentType = TypeOfHomework.Rar,
                SubmissionDate = new DateTime(2015, 3, 14)
            };
            

            //fill few resources
            Resource databaseLab = new Resource()
            {
                Name = "Database Applications Lab",
                ResourceType = TypeOfResource.Document,
                Url = "https://softuni.bg/downloads/svn/db-apps/July-2015/2.%20Entity-Framework-Code-First-Exercise-Book-Shop.zip"
            };

            Resource databaseLecture = new Resource()
            {
                Name = "Database Applications: EF Code First Lecture",
                ResourceType = TypeOfResource.Video,
                Url = "https://softuni.bg/Trainings/Resources/Video/5137/Video-16-July-2015-Atanas-Rusenov-Database-Applications-Jul-2015"
            };

            //add relationships between sudents and courses
            petar.Courses = new HashSet<Course>{cSharp, databaseApps};
            mincho.Courses = new HashSet<Course> {databaseApps, htmlAndCss};
            mitio.Courses = new HashSet<Course> {databaseApps, htmlAndCss};

            //add relationships between courses and students
            cSharp.Students = new HashSet<Student> {petar}; ;
            databaseApps.Students = new HashSet<Student>{petar, mincho, mitio};
            htmlAndCss.Students = new HashSet<Student> {mincho, mitio };

            //add relationship between students and homeworks
            petar.Homeworks = new HashSet<Homework>{petarHomework};
            mincho.Homeworks = new HashSet<Homework> {minchoHomework};

            //add relationships between courses and homeworks
            databaseApps.Homeworks = new HashSet<Homework>{petarHomework};
            htmlAndCss.Homeworks = new HashSet<Homework> { minchoHomework };

            //add relationships between courses and resourses
            databaseApps.Resources = new HashSet<Resource> {databaseLab, databaseLecture};

            context.Students.AddOrUpdate(petar, mitio, mincho);
            context.Courses.AddOrUpdate(cSharp, databaseApps, htmlAndCss);
            context.Homeworks.AddOrUpdate(petarHomework, minchoHomework);
            context.Resources.AddOrUpdate(databaseLab, databaseLecture);

            context.SaveChanges();
        }
    }
}
