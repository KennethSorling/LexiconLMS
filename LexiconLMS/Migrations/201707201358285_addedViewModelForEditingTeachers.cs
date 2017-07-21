namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedViewModelForEditingTeachers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditTeacherAccountVMs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EditTeacherAccountVMs");
        }
    }
}
