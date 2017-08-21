namespace LexiconLMS.Migrations
{
    using LexiconLMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LexiconLMS.Models.ApplicationDbContext context)
        {
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "E-Learning" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Lecture" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Code-Along" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Project" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Exercise" });

            string loremipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            string loremipshort = "Lorem ipsum dolor sit amet";

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 1,
                Name = ".Net Developer",
                Description = loremipsum,
                StartDate = new DateTime(2017, 7, 3),
                EndDate = new DateTime(2017, 10, 27, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 2,
                Name = "Java Developer",
                Description = loremipsum,
                StartDate = new DateTime(2017, 6, 26),
                EndDate = new DateTime(2017, 10, 20, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 3,
                Name = "Excel",
                Description = loremipsum,
                StartDate = new DateTime(2017, 6, 19),
                EndDate = new DateTime(2017, 10, 13, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 4,
                Name = "PowerPoint",
                Description = loremipsum,
                StartDate = new DateTime(2017, 6, 12),
                EndDate = new DateTime(2017, 10, 6, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 5,
                Name = "Outlook",
                Description = loremipsum,
                StartDate = new DateTime(2017, 6, 15),
                EndDate = new DateTime(2017, 9, 29, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 6,
                Name = "OneNote",
                Description = loremipsum,
                StartDate = new DateTime(2017, 6, 8),
                EndDate = new DateTime(2017, 9, 22, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 7,
                Name = "Access",
                Description = loremipsum,
                StartDate = new DateTime(2017, 6, 1),
                EndDate = new DateTime(2017, 9, 15, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 8,
                Name = "Web och App",
                Description = loremipsum,
                StartDate = new DateTime(2017, 5, 26),
                EndDate = new DateTime(2017, 9, 8, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 9,
                Name = "SharePoint",
                Description = loremipsum,
                StartDate = new DateTime(2017, 5, 19),
                EndDate = new DateTime(2017, 9, 1, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 10,
                Name = "Grafisk Produktion",
                Description = loremipsum,
                StartDate = new DateTime(2017, 5, 19),
                EndDate = new DateTime(2017, 9, 1, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 11,
                Name = "Crystal Reports",
                Description = loremipsum,
                StartDate = new DateTime(2017, 5, 19),
                EndDate = new DateTime(2017, 9, 1, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 12,
                Name = "JAVA for Dummies",
                Description = loremipsum,
                StartDate = new DateTime(2017, 5, 19),
                EndDate = new DateTime(2017, 9, 1, 17, 0, 0),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 1,
                CourseId = 1,
                Name = "C#",
                Description = loremipsum,
                StartDate = new DateTime(2017, 7, 3),
                EndDate = new DateTime(2017, 7, 28, 17, 0, 0),
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 2,
                CourseId = 1,
                Name = "Web",
                Description = loremipsum,
                StartDate = new DateTime(2017, 7, 31),
                EndDate = new DateTime(2017, 8, 11, 17, 0, 0)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 3,
                CourseId = 1,
                Name = "MVC",
                Description = loremipsum,
                StartDate = new DateTime(2017, 8, 14),
                EndDate = new DateTime(2017, 8, 25, 17, 0, 0)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 4,
                CourseId = 1,
                Name = "Database",
                Description = loremipsum,
                StartDate = new DateTime(2017, 8, 28),
                EndDate = new DateTime(2017, 9, 1, 17, 0, 0)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 5,
                CourseId = 1,
                Name = "Testing",
                Description = loremipsum,
                StartDate = new DateTime(2017, 9, 4),
                EndDate = new DateTime(2017, 9, 11, 17, 0, 0)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 6,
                CourseId = 1,
                Name = "Application Dev.",
                Description = loremipsum,
                StartDate = new DateTime(2017, 9, 12),
                EndDate = new DateTime(2017, 9, 18, 17, 0, 0)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 7,
                CourseId = 1,
                Name = "MVC Advanced",
                Description = loremipsum,
                StartDate = new DateTime(2017, 9, 19),
                EndDate = new DateTime(2017, 10, 27, 17, 0, 0)
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 1,
                ModuleId = 1,
                Name = "Introduction, 1.1, 1.2",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 3, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 3, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 2,
                ModuleId = 1,
                Name = "1.3",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 4, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 4, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 3,
                ModuleId = 1,
                Name = "1.4, 1.5",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 4, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 4, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 4,
                ModuleId = 1,
                Name = "C# Intro",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 5, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 5, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 5,
                ModuleId = 1,
                Name = "Exercise 1",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 6, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 6, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 6,
                ModuleId = 1,
                Name = "2",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 6, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 6, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 7,
                ModuleId = 1,
                Name = "C# Basics",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 7, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 7, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 8,
                ModuleId = 1,
                Name = "C# Basics",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 10, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 10, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 9,
                ModuleId = 1,
                Name = "1.6, 1.7",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 11, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 11, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 10,
                ModuleId = 1,
                Name = "1.8",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 11, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 11, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 11,
                ModuleId = 1,
                Name = "1.7, 1.8",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 12, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 12, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 12,
                ModuleId = 1,
                Name = "2/Repetition",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 12, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 12, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 13,
                ModuleId = 1,
                Name = "OOP",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 13, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 13, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 14,
                ModuleId = 1,
                Name = "3",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 14, 8, 30, 0),
                EndDate = new DateTime(2017, 7, 14, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 17,
                ModuleId = 1,
                Name = "OOP 2",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 17, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 17, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 18,
                ModuleId = 1,
                Name = "Exercise 3",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 18, 8, 30, 0),
                EndDate = new DateTime(2017, 7, 18, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 19,
                ModuleId = 1,
                Name = "2.1 - 2.4",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 19, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 19, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 20,
                ModuleId = 1,
                Name = "Exercise 4",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 19, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 19, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 21,
                ModuleId = 1,
                Name = "Generics",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 20, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 20, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 22,
                ModuleId = 1,
                Name = "2.5 - 2.6",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 21, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 21, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 23,
                ModuleId = 1,
                Name = "4",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 21, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 21, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 24,
                ModuleId = 1,
                Name = "2.7 - 2.9",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 24, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 24, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 25,
                ModuleId = 1,
                Name = "Exercise 4",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 24, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 24, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 26,
                ModuleId = 1,
                Name = "Generics",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 25, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 25, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 27,
                ModuleId = 1,
                Name = "LINQ",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 25, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 25, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 28,
                ModuleId = 1,
                Name = "Garage 1.0 Intro",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 26, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 26, 12, 0, 0),
                External = true
            });


            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 29,
                ModuleId = 1,
                Name = "Garage 1.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 26, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 26, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 30,
                ModuleId = 1,
                Name = "Garage 1.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 27, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 27, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 31,
                ModuleId = 1,
                Name = "Garage 1.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 28, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 28, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 32,
                ModuleId = 1,
                Name = "Garage 1.0 reporting",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 28, 13, 0, 0),
                EndDate = new DateTime(2017, 7, 28, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 33,
                ModuleId = 2,
                Name = "HTML/CSS",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 7, 31, 08, 30, 0),
                EndDate = new DateTime(2017, 7, 31, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 34,
                ModuleId = 2,
                Name = "3 + 4.1 - 4.3",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 1, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 1, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 35,
                ModuleId = 2,
                Name = "HTML",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 1, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 1, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 36,
                ModuleId = 2,
                Name = "4.4 - 4.5",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 2, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 2, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 37,
                ModuleId = 2,
                Name = "CSS",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 2, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 2, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 38,
                ModuleId = 2,
                Name = "JavaScript",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 3, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 3, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 39,
                ModuleId = 2,
                Name = "5.1 - 5.3",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 4, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 4, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 40,
                ModuleId = 2,
                Name = "5.4 - 5.5",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 4, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 4, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 41,
                ModuleId = 2,
                Name = "JavaScript",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 7, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 7, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 42,
                ModuleId = 2,
                Name = "6, Bootstrap",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 8, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 8, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 43,
                ModuleId = 2,
                Name = "Bootstrap",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 9, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 9, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 44,
                ModuleId = 2,
                Name = "GIT",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 10, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 10, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 45,
                ModuleId = 2,
                Name = "Bootstrap",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 11, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 11, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 46,
                ModuleId = 3,
                Name = "ASP.NET MVC",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 14, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 14, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 47,
                ModuleId = 3,
                Name = "7.1 - 7.3",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 15, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 15, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 48,
                ModuleId = 3,
                Name = "7.4 - 7.5",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 16, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 16, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 49,
                ModuleId = 3,
                Name = "MVC Basics",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 16, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 16, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 50,
                ModuleId = 3,
                Name = "ASP.NET MVC",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 17, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 17, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 51,
                ModuleId = 3,
                Name = "7.6",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 18, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 18, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 52,
                ModuleId = 3,
                Name = "MVC",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 18, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 18, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 53,
                ModuleId = 3,
                Name = "7.6",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 21, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 21, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 54,
                ModuleId = 3,
                Name = "MVC",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 21, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 21, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 55,
                ModuleId = 3,
                Name = "Garage 2.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 22, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 22, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 56,
                ModuleId = 3,
                Name = "Garage 2.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 23, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 23, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 57,
                ModuleId = 3,
                Name = "Garage 2.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 24, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 24, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 58,
                ModuleId = 3,
                Name = "Garage 2.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 25, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 25, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 59,
                ModuleId = 3,
                Name = "Report Garage 2.0",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 25, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 25, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 60,
                ModuleId = 4,
                Name = "Data Modeling",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 28, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 28, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 61,
                ModuleId = 4,
                Name = "13",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 28, 13, 0, 0),
                EndDate = new DateTime(2017, 8, 28, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 62,
                ModuleId = 4,
                Name = "Entity Framework",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 29, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 29, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 63,
                ModuleId = 4,
                Name = "Garage 2.5",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 30, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 30, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 64,
                ModuleId = 4,
                Name = "Garage 2.5",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 8, 31, 08, 30, 0),
                EndDate = new DateTime(2017, 8, 31, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 65,
                ModuleId = 4,
                Name = "SQLBolt.com",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 1, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 1, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 66,
                ModuleId = 5,
                Name = "9, TDD",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 4, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 4, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 67,
                ModuleId = 5,
                Name = "9, TDD",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 5, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 5, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 68,
                ModuleId = 5,
                Name = "ISTQB",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 6, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 6, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 69,
                ModuleId = 5,
                Name = "ISTQB",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 7, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 7, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 70,
                ModuleId = 5,
                Name = "ISTQB",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 8, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 8, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 71,
                ModuleId = 5,
                Name = "ISTQB",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 11, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 11, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 72,
                ModuleId = 6,
                Name = "11, JS Framework",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 12, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 12, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 73,
                ModuleId = 6,
                Name = "11, JS Framework",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 13, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 13, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 74,
                ModuleId = 6,
                Name = "UX",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 14, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 14, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 75,
                ModuleId = 6,
                Name = "16, UX",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 14, 13, 0, 0),
                EndDate = new DateTime(2017, 9, 14, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 76,
                ModuleId = 6,
                Name = "10, UX",
                ActivityTypeId = 1,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 15, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 15, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 77,
                ModuleId = 6,
                Name = "Client vs. Server",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 18, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 18, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 78,
                ModuleId = 7,
                Name = "11, Repetition",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 19, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 19, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 79,
                ModuleId = 7,
                Name = "12, MVC",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 19, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 19, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 80,
                ModuleId = 7,
                Name = "Identity",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 20, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 20, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 81,
                ModuleId = 7,
                Name = "Identity",
                ActivityTypeId = 5,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 21, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 21, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 82,
                ModuleId = 7,
                Name = "SCRUM",
                ActivityTypeId = 2,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 22, 08, 30, 0),
                EndDate = new DateTime(2017, 9, 22, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 83,
                ModuleId = 7,
                Name = "Project Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 25, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 25, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 84,
                ModuleId = 7,
                Name = "Project Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 26, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 26, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 85,
                ModuleId = 7,
                Name = "Project Start",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 27, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 27, 12, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 86,
                ModuleId = 7,
                Name = "Sprint 1 Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 27, 13, 0, 0),
                EndDate = new DateTime(2017, 9, 27, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 87,
                ModuleId = 7,
                Name = "Sprint 1",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 28, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 28, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 88,
                ModuleId = 7,
                Name = "Sprint 1",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 9, 29, 8, 30, 0),
                EndDate = new DateTime(2017, 9, 29, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 89,
                ModuleId = 7,
                Name = "Sprint 1",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 2, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 2, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 90,
                ModuleId = 7,
                Name = "Sprint 1",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 3, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 3, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 91,
                ModuleId = 7,
                Name = "Sprint 1 Review",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 4, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 4, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 92,
                ModuleId = 7,
                Name = "Sprint 2 Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 4, 13, 0, 0),
                EndDate = new DateTime(2017, 10, 4, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 93,
                ModuleId = 7,
                Name = "Sprint 2",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 5, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 5, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 94,
                ModuleId = 7,
                Name = "Sprint 2",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 6, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 6, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 95,
                ModuleId = 7,
                Name = "Sprint 2",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 9, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 9, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 96,
                ModuleId = 7,
                Name = "Sprint 2",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 10, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 10, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 97,
                ModuleId = 7,
                Name = "Sprint 2 Review",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 11, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 11, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 98,
                ModuleId = 7,
                Name = "Sprint 3 Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 11, 13, 0, 0),
                EndDate = new DateTime(2017, 10, 11, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 99,
                ModuleId = 7,
                Name = "Sprint 3",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 12, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 12, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 100,
                ModuleId = 7,
                Name = "Sprint 3",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 13, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 13, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 101,
                ModuleId = 7,
                Name = "Sprint 3",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 16, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 16, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 102,
                ModuleId = 7,
                Name = "Sprint 3",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 17, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 17, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 103,
                ModuleId = 7,
                Name = "Sprint 3 Review",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 18, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 18, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 104,
                ModuleId = 7,
                Name = "Sprint 4 Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 18, 13, 0, 0),
                EndDate = new DateTime(2017, 10, 18, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 105,
                ModuleId = 7,
                Name = "Sprint 4",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 19, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 19, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 106,
                ModuleId = 7,
                Name = "Sprint 4",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 20, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 20, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 107,
                ModuleId = 7,
                Name = "Sprint 4",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 23, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 23, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 108,
                ModuleId = 7,
                Name = "Sprint 4",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 24, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 24, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 109,
                ModuleId = 7,
                Name = "Sprint 4 Review",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 25, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 25, 17, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 110,
                ModuleId = 7,
                Name = "Demo Planning",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 26, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 26, 17, 0, 0),
                External = false
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 111,
                ModuleId = 7,
                Name = "Demo",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 27, 8, 30, 0),
                EndDate = new DateTime(2017, 10, 27, 12, 0, 0),
                External = true
            });

            context.Activities.AddOrUpdate(a => a.Id, new Activity
            {
                Id = 112,
                ModuleId = 7,
                Name = "Graduation",
                ActivityTypeId = 4,
                Description = loremipshort,
                StartDate = new DateTime(2017, 10, 27, 13, 0, 0),
                EndDate = new DateTime(2017, 10, 27, 17, 0, 0),
                External = true
            });

            context.SaveChanges();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            ApplicationUser user;
            IdentityRole teacherRole, studentRole;
            IdentityResult result;

            if (roleManager.FindByName("Teacher") == null)
            {
                teacherRole = new IdentityRole("Teacher");
                result = roleManager.Create(teacherRole);
            }

            context.SaveChanges();

            if (roleManager.FindByName("Student") == null)
            {
                studentRole = new IdentityRole("Student");
                result = roleManager.Create(studentRole);
            }

            if (userManager.FindByEmail("oscar.jakobsson@lexicon.se") == null)
            {
                user = new Teacher
                {
                    FirstName = "Oscar",
                    LastName = "Jakobsson",
                    Email = "oscar.jakobsson@lexicon.se",
                    UserName = "oscar.jakobsson@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Teacher");
            }

            if (userManager.FindByEmail("john.hellman@lexicon.se") == null)
            {
                user = new Teacher
                {
                    FirstName = "John",
                    LastName = "Hellman",
                    Email = "john.hellman@lexicon.se",
                    UserName = "john.hellman@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Teacher");
            }

            if (userManager.FindByEmail("dmitris.bjorlingh@lexicon.se") == null)
            {
                user = new Teacher
                {
                    FirstName = "Dmitris",
                    LastName = "Björlingh",
                    Email = "dmitris.bjorlingh@lexicon.se",
                    UserName = "dmitris.bjorlingh@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Teacher");
            }

            if (userManager.FindByEmail("student.studentsson@lexicon.se") == null)
            {
                user = new Student
                {
                    FirstName = "Student",
                    LastName = "Studentsson",
                    Email = "student.studentsson@lexicon.se",
                    CourseId = 1,
                    UserName = "student.studentsson@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Student");
            }

            if (userManager.FindByEmail("daniel.larusso@lexicon.se") == null)
            {
                user = new Student
                {
                    FirstName = "Daniel",
                    LastName = "LaRusso",
                    Email = "daniel.larusso@lexicon.se",
                    CourseId = 1,
                    UserName = "daniel.larusso@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Student");
            }

            if (userManager.FindByEmail("kwaichang.caine@lexicon.se") == null)
            {
                user = new Student
                {
                    FirstName = "Kwai Chang",
                    LastName = "Caine",
                    Email = "kwaichang.caine@lexicon.se",
                    CourseId = 2,
                    UserName = "kwaichang.caine@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Student");
            }

            if (userManager.FindByEmail("forrest.gump@lexicon.se") == null)
            {
                user = new Student
                {
                    FirstName = "Forrest",
                    LastName = "Gump",
                    Email = "forrest.gump@lexicon.se",
                    CourseId = 2,
                    UserName = "forrest.gump@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Student");
            }

            if (userManager.FindByEmail("biff.henderson@lexicon.se") == null)
            {
                user = new Student
                {
                    FirstName = "Biff",
                    LastName = "Henderson",
                    Email = "biff.henderson@lexicon.se",
                    CourseId = 3,
                    UserName = "biff.henderson@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Student");
            }

            context.SaveChanges();
        }
    }
}
