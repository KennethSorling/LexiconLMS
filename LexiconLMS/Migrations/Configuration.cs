namespace LexiconLMS.Migrations
{
    using LexiconLMS.Models;
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
        }
    }
}
