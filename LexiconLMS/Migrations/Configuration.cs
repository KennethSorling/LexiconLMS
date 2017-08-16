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

            string loremipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            string loremipshort = "Lorem ipsum dolor sit amet";
            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Java for dummies",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now,
                DateChanged = DateTime.Now, Description = "Booga"
            });

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = ".Net Developer",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Java Developer",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();


            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Excel",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });


            context.SaveChanges();

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "PowerPoint",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });


            context.SaveChanges();

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Outlook",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();


            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "OneNote",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();


            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Access",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });


            context.SaveChanges();



            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Web och App",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });


            context.SaveChanges();

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "SharePoint",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();


            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Grafisk Produktion",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Crystal Reports",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();

            context.Modules.AddOrUpdate(m => m.Name, new Module
            {
                CourseId = 1,
                Name = "C# Basics",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            });

            context.Modules.AddOrUpdate(m => m.Name, new Module
            {
                CourseId = 1,
                Name = "C# Advanced",
                Description = loremipsum,
                StartDate = DateTime.Now.AddMonths(1).AddDays(1),
                EndDate = DateTime.Now.AddMonths(2)
            });

            context.Modules.AddOrUpdate(m => m.Name, new Module
            {
                CourseId = 1,
                Name = "MVC Basics",
                Description = loremipsum,
                StartDate = DateTime.Now.AddMonths(2).AddDays(1),
                EndDate = DateTime.Now.AddMonths(3)
            });

            context.Modules.AddOrUpdate(m => m.Name, new Module
            {
                CourseId = 1,
                Name = "MVC Advanced",
                Description = loremipsum,
                StartDate = DateTime.Now.AddMonths(3).AddDays(1),
                EndDate = DateTime.Now.AddMonths(4)
            });

            context.SaveChanges();

            TimeSpan courseSpan;
            courseSpan = DateTime.Now.AddMonths(4) - DateTime.Now;
            int numberOfDays = int.Parse((courseSpan.Days + 1).ToString());

            for (int i = 0; i < numberOfDays; i++)
            {
                if (i < 30)
                {
                    context.Activities.AddOrUpdate(m => m.Name, new Activity
                    {
                        ModuleId = 1,
                        Name = "Introduction",
                        ActivityTypeId = 1,
                        Description = loremipshort,
                        StartDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 8, 30, 0),
                        EndDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 17, 0, 0),
                        External = false
                    });
                }
                else if (i < 60)
                {
                    context.Activities.AddOrUpdate(m => m.Name, new Activity
                    {
                        ModuleId = 1,
                        Name = "Intermediate",
                        ActivityTypeId = 2,
                        Description = loremipshort,
                        StartDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 8, 30, 0),
                        EndDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 17, 0, 0),
                        External = false
                    });
                }
                else if (i < 90)
                {
                    context.Activities.AddOrUpdate(m => m.Name, new Activity
                    {
                        ModuleId = 1,
                        Name = "Advanced",
                        ActivityTypeId = 3,
                        Description = loremipshort,
                        StartDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 8, 30, 0),
                        EndDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 17, 0, 0),
                        External = true
                    });
                }
                else
                {
                    context.Activities.AddOrUpdate(m => m.Name, new Activity
                    {
                        ModuleId = 1,
                        Name = "Professional",
                        ActivityTypeId = 4,
                        Description = loremipshort,
                        StartDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 8, 30, 0),
                        EndDate = new DateTime(DateTime.Now.AddDays(i).Year, DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day, 17, 0, 0),
                        External = true
                    });
                }
            }

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

                user = new Teacher
                {
                    FirstName = "John",
                    LastName = "Hellman",
                    Email = "john.hellman@lexicon.se",
                    UserName = "john.hellman@lexicon.se"
                };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Teacher");

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

            context.Courses.AddOrUpdate(c => c.Name, new Course { Name = "Java for dummies", StartDate = System.DateTime.Now, EndDate = System.DateTime.Now, DateChanged = System.DateTime.Now, Description = "Booga" });

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
