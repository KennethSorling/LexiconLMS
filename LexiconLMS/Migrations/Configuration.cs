namespace LexiconLMS.Migrations
{
    using LexiconLMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LexiconLMS.Models.ApplicationDbContext context)
        {

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            ApplicationUser user;
            IdentityRole teacherRole, studentRole;
            IdentityResult result;
            string userId;

            if (roleManager.FindByName("Teacher") == null)
            {
                teacherRole = new IdentityRole("Teacher");
                result = roleManager.Create(teacherRole);
            }

            if (roleManager.FindByName("Student") == null)
            {
                studentRole = new IdentityRole("Student");
                result = roleManager.Create(studentRole);
            }

            var teachers = new List<Teacher>
            {
                new Teacher{FirstName = "Oscar", LastName = "Jakobsson",Email = "oscar.jakobsson@lexicon.se",UserName = "oscar.jakobsson@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)},
                new Teacher{FirstName = "John",LastName = "Hellman",Email = "john.hellman@lexicon.se",UserName = "john.hellman@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)},
                new Teacher{FirstName = "Dmitris",LastName = "Björlingh",Email = "dmitris.bjorlingh@lexicon.se",UserName = "dmitris.bjorlingh@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)}
            };

            foreach (var teacher in teachers)
            {
                user = userManager.FindByEmail(teacher.Email);
                if (user == null)
                {
                    user = teacher;
                    result = userManager.Create(user, "VerySecret123!");
                }
                if (user.Roles.Count == 0)
                {
                    result = userManager.AddToRole(user.Id, "Teacher");
                }
                userId = teacher.Id;
            }

            var activityTypes = new List<ActivityType>
            {
                new ActivityType { Id = 1, TypeName = "E-Learning" },
                new ActivityType { Id = 2, TypeName = "Lecture" },
                new ActivityType { Id = 3, TypeName = "Code-Along" },
                new ActivityType { Id = 4, TypeName = "Project" },
                new ActivityType { Id = 5, TypeName = "Exercise" }
            };

            foreach (var activityType in activityTypes)
            {
                context.ActivityTypes.AddOrUpdate(a => a.Id, activityType);
            }

            var purposes = new List<Purpose>
            {
                new Purpose { Id = 1, Name = "General" },
                new Purpose { Id = 2, Name = "Course Description" },
                new Purpose { Id = 3, Name = "Module Description" },
                new Purpose { Id = 4, Name = "Activity Description" },
                new Purpose { Id = 5, Name = "Assignment Description" },
                new Purpose { Id = 6, Name = "Useful Links" },
                new Purpose { Id = 7, Name = "Student Hand-In" }
            };
            foreach (var purpose in purposes)
            {
                context.Purposes.AddOrUpdate(p => p.Id, purpose);
            }

            var statuses = new List<Status>
            {
                new Status { Id = 1, Name = "Issued" },
                new Status { Id = 2, Name = "Pending" },
                new Status { Id = 3, Name = "Submitted" },
                new Status { Id = 4, Name = "Reviewed" },
                new Status { Id = 5, Name = "Approved" },
                new Status { Id = 6, Name = "Failed" },
                new Status { Id = 7, Name = "Deleted" }
            };
            foreach (var status in statuses)
            {
                context.Statuses.AddOrUpdate(p => p.Id, status);
            }

            var mimeTypes = new List<MimeType>
            {
                new MimeType{ Id = 1, DefaultExtension = "txt",Name = "text/plain"},
                new MimeType{ Id = 2, DefaultExtension = "png",Name = "image/png"},
                new MimeType{ Id = 3, DefaultExtension = "jpg",Name = "image/jpeg"},
                new MimeType{ Id = 4, DefaultExtension = "pdf",Name = "application/pdf"},
                new MimeType{ Id = 5, DefaultExtension = "zip",Name = "application/zip"},
                new MimeType{ Id = 6, DefaultExtension = "rar",Name = "application/x-rar-compressed"},
                new MimeType{ Id = 7, DefaultExtension = "doc",Name = "application/ms-word"},
                new MimeType{ Id = 8, DefaultExtension = "ppt",Name = "application/vnd.ms-powerpoint"},
                new MimeType{ Id = 9, DefaultExtension = "xls",Name = "application/vnd.ms-excel"},
                new MimeType{ Id = 10, DefaultExtension = "docx",Name = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                new MimeType{ Id = 11, DefaultExtension = "xlsx",Name = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                new MimeType{ Id = 12, DefaultExtension = "pptx",Name = "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                new MimeType{ Id = 13, DefaultExtension = "", Name = "application/octet-stream" },
                new MimeType { Id = 14, DefaultExtension = "cs", Name="text/x-csharp" },
                new MimeType { Id = 15, DefaultExtension = "html", Name="text/html" },
                new MimeType { Id = 16, DefaultExtension = "htm", Name="text/html" },
            };

            foreach (var mimeType in mimeTypes)
            {
                context.MimeTypes.AddOrUpdate(m => m.Id, mimeType);
            }

            context.SaveChanges();

            string loremipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            string loremipshort = "Lorem ipsum dolor sit amet";

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 1,
                Name = ".Net Developer",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 2,
                Name = "Java Developer",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 3,
                Name = "Excel",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 4,
                Name = "PowerPoint",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 5,
                Name = "Outlook",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 6,
                Name = "OneNote",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 7,
                Name = "Access",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 8,
                Name = "Web och App",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 9,
                Name = "SharePoint",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 10,
                Name = "Grafisk Produktion",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 11,
                Name = "Crystal Reports",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.Courses.AddOrUpdate(c => c.Id, new Course
            {
                Id = 12,
                Name = "JAVA for Dummies",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                DateChanged = DateTime.Now
            });

            context.SaveChanges();

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 1,
                CourseId = 1,
                Name = "C# Basics",
                Description = loremipsum,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 2,
                CourseId = 1,
                Name = "C# Advanced",
                Description = loremipsum,
                StartDate = DateTime.Now.AddMonths(1).AddDays(1),
                EndDate = DateTime.Now.AddMonths(2)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 3,
                CourseId = 1,
                Name = "MVC Basics",
                Description = loremipsum,
                StartDate = DateTime.Now.AddMonths(2).AddDays(1),
                EndDate = DateTime.Now.AddMonths(3)
            });

            context.Modules.AddOrUpdate(m => m.Id, new Module
            {
                Id = 4,
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
                var thatDay = DateTime.Now.AddDays(i);
                if (i < 30)
                {
                    context.Activities.AddOrUpdate(a => a.Id, new Activity
                    {
                        Id = i,
                        ModuleId = 1,
                        Name = "Introduction",
                        ActivityTypeId = 1,
                        Description = loremipshort,
                        StartDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 8, 30, 0),
                        EndDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 17, 0, 0),
                        External = false
                    });
                }
                else if (i >= 30 && i < 60)
                {
                    context.Activities.AddOrUpdate(a => a.Id, new Activity
                    {
                        Id = i,
                        ModuleId = 2,
                        Name = "Intermediate",
                        ActivityTypeId = 2,
                        Description = loremipshort,
                        StartDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 8, 30, 0),
                        EndDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 17, 0, 0),
                        External = false
                    });
                }
                else if (i >= 60 && i < 90)
                {
                    context.Activities.AddOrUpdate(a => a.Id, new Activity
                    {
                        Id = i,
                        ModuleId = 3,
                        Name = "Advanced",
                        ActivityTypeId = 3,
                        Description = loremipshort,
                        StartDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 8, 30, 0),
                        EndDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 17, 0, 0),
                        External = true
                    });
                }
                else
                {
                    context.Activities.AddOrUpdate(a => a.Id, new Activity
                    {
                        Id = i,
                        ModuleId = 4,
                        Name = "Professional",
                        ActivityTypeId = 4,
                        Description = loremipshort,
                        StartDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 8, 30, 0),
                        EndDate = new DateTime(thatDay.Year, thatDay.Month, thatDay.Day, 17, 0, 0),
                        External = true
                    });
                }
            }

            context.SaveChanges();


            var students = new List<Student>
            {
                new Student{FirstName = "Student",LastName = "Studentsson",Email = "student.studentsson@lexicon.se",CourseId = 1,UserName = "student.studentsson@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)},
                new Student{FirstName = "Kwai Chang",LastName = "Caine",Email = "kwaichang.caine@lexicon.se",CourseId = 1,UserName = "kwaichang.caine@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)},
                new Student{FirstName = "Forrest",LastName = "Gump",Email = "forrest.gump@lexicon.se",CourseId = 1,UserName = "forrest.gump@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)},
                new Student{FirstName = "Biff",LastName = "Henderson",Email = "biff.henderson@lexicon.se",CourseId = 1,UserName = "biff.henderson@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)},
                new Student{FirstName = "Daniel",LastName = "LaRusso",Email = "daniel.larusso@lexicon.se",CourseId = 1,UserName = "daniel.larusso@lexicon.se", LastLogon=DateTime.Now.AddDays(-2)}
            };

            foreach (var student in students)
            {
                user = student;
                if (userManager.FindByEmail(user.Email) == null)
                {
                    result = userManager.Create(user, "VerySecret123!");
                    result = userManager.AddToRole(user.Id, "Student");
                }
            }
            
            context.SaveChanges();

            context.Documents.AddOrUpdate(d => d.Filename, new Document
            {

                Filename = "Course Description .Net.pdf",
                FileSize = 130452,
                FileType = "application/pdf",
                CourseId = 1,
                MimeType = mimeTypes.Find(m => m.DefaultExtension == "pdf"),
                MimeTypeId = mimeTypes.Find(m => m.DefaultExtension == "pdf").Id,
                Status = new Status { Id = 1, Name = "Issued" },
                Purpose = context.Purposes.Find(2),
                StatusId = 1,
                Title = "Course Description .Net",
                OwnerId = teachers[1].Id,
                Owner = teachers[1],
                DateUploaded = DateTime.Now

            });

            context.SaveChanges();

            context.Documents.AddOrUpdate(d => d.Filename, new Document
            {
                Filename = "Assignment 1.1.txt",
                FileSize = 174,
                ActivityId = 1,
                //MimeType = new MimeType { Id = 4, Name = "application/pdf" },
                MimeType = context.MimeTypes.Find(1),
                Status = context.Statuses.Find(1),
                Purpose = context.Purposes.Find(5),
                PurposeId = 5,
                Title = "Assignment 1.1",
                OwnerId = teachers[1].Id,
                DeadLine = DateTime.Now.AddDays(4).Date,
                DateUploaded = DateTime.Now

            });

            context.SaveChanges();

            context.Documents.AddOrUpdate(d => d.Filename, new Document
            {
                Filename = "My pathetic attempt.zip",
                FileSize = 223,
                ActivityId = 1,
                MimeType = context.MimeTypes.Find(5),
                Status = context.Statuses.Find(5),
                Title = "My Pathetic Attempt",
                PurposeId = 7,
                Purpose = context.Purposes.Find(7),
                OwnerId = students[1].Id,
                DateUploaded = DateTime.Now
            });

            context.SaveChanges();

        }
    }
}
