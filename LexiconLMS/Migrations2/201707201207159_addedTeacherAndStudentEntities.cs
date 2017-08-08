namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTeacherAndStudentEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CreateStudentAccountVM_Id", "dbo.CreateTeacherAccountVMs");
            DropIndex("dbo.Courses", new[] { "CreateStudentAccountVM_Id" });
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Courses", "CreateStudentAccountVM_Id");
            DropTable("dbo.CreateTeacherAccountVMs");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Courses", "CreateStudentAccountVM_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Discriminator");
            CreateIndex("dbo.Courses", "CreateStudentAccountVM_Id");
            AddForeignKey("dbo.Courses", "CreateStudentAccountVM_Id", "dbo.CreateTeacherAccountVMs", "Id");
        }
    }
}
