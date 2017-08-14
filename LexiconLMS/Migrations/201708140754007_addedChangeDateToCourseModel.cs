namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedChangeDateToCourseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "DateChanged", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "DateChanged");
        }
    }
}
