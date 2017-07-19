namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSeedCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        ActivityTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateApproved = c.DateTime(),
                        External = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId)
                .Index(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            AddColumn("dbo.Documents", "Activity_Id", c => c.Int());
            CreateIndex("dbo.Documents", "ModuleId");
            CreateIndex("dbo.Documents", "CourseId");
            CreateIndex("dbo.Documents", "Activity_Id");
            AddForeignKey("dbo.Documents", "Activity_Id", "dbo.Activities", "Id");
            AddForeignKey("dbo.Documents", "CourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.Documents", "ModuleId", "dbo.Modules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Documents", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Documents", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Documents", "Activity_Id", "dbo.Activities");
            DropIndex("dbo.Modules", new[] { "CourseId" });
            DropIndex("dbo.Documents", new[] { "Activity_Id" });
            DropIndex("dbo.Documents", new[] { "CourseId" });
            DropIndex("dbo.Documents", new[] { "ModuleId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            DropColumn("dbo.Documents", "Activity_Id");
            DropTable("dbo.Modules");
            DropTable("dbo.Courses");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Activities");
        }
    }
}
