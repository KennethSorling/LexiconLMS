namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorStudentType : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Courses", new[] { "Course_Id" });
            AddColumn("dbo.AspNetUsers", "Course_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Course_Id");
            DropColumn("dbo.Courses", "Course_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Course_Id", c => c.Int());
            DropIndex("dbo.AspNetUsers", new[] { "Course_Id" });
            DropColumn("dbo.AspNetUsers", "Course_Id");
            CreateIndex("dbo.Courses", "Course_Id");
        }
    }
}
