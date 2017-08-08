namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateedVMforStudentCreation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CreateStudentAccountVM_Id", "dbo.CreateTeacherAccountVMs");
            DropIndex("dbo.Courses", new[] { "CreateStudentAccountVM_Id" });
            DropColumn("dbo.Courses", "CreateStudentAccountVM_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CreateStudentAccountVM_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "CreateStudentAccountVM_Id");
            AddForeignKey("dbo.Courses", "CreateStudentAccountVM_Id", "dbo.CreateTeacherAccountVMs", "Id");
        }
    }
}
