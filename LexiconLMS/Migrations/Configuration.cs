namespace LexiconLMS.Migrations
{
    using LexiconLMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Assignment" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Code-Along" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "APL" });
            context.ActivityTypes.AddOrUpdate(a => a.TypeName, new ActivityType { TypeName = "Project" });

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
                result =  roleManager.Create(teacherRole);
            }
            if (roleManager.FindByName("Student") == null)
            {
                studentRole = new IdentityRole("Student");
                result = roleManager.Create(studentRole);
            }
            if (userManager.FindByEmail("oscar.jakobsson@lexicon.se") == null)
            {
                user = new Teacher {
                        FirstName = "Oscar",
                        LastName = "Jakobsson",
                        Email = "oscar.jakobsson@lexicon.se",
                        UserName = "oscar.jakobsson@lexicon.se"
                    };
                result = userManager.Create(user, "VerySecret123!");
                result = userManager.AddToRole(user.Id, "Teacher");
            }

            context.Courses.AddOrUpdate(c => c.Name, new Course { Name = "Java for dummies", StartDate = System.DateTime.Today, EndDate = System.DateTime.Today, Description = "Booga" });

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

        }
    }
}
