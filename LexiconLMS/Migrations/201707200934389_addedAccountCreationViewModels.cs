namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAccountCreationViewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateTeacherAccountVMs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        CourseId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "Course_Id", c => c.Int());
            AddColumn("dbo.Courses", "CreateStudentAccountVM_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "Course_Id");
            CreateIndex("dbo.Courses", "CreateStudentAccountVM_Id");
            AddForeignKey("dbo.Courses", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Courses", "CreateStudentAccountVM_Id", "dbo.CreateTeacherAccountVMs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CreateStudentAccountVM_Id", "dbo.CreateTeacherAccountVMs");
            DropForeignKey("dbo.Courses", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "CreateStudentAccountVM_Id" });
            DropIndex("dbo.Courses", new[] { "Course_Id" });
            DropColumn("dbo.Courses", "CreateStudentAccountVM_Id");
            DropColumn("dbo.Courses", "Course_Id");
            DropTable("dbo.CreateTeacherAccountVMs");
        }
    }
}
