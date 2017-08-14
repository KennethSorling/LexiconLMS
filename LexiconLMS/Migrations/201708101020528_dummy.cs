namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dummy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DashboardVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        StudentName = c.String(),
                        TodaysDate = c.DateTime(nullable: false),
                        ModuleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Activities", "DashboardVM_Id", c => c.Int());
            CreateIndex("dbo.Activities", "DashboardVM_Id");
            AddForeignKey("dbo.Activities", "DashboardVM_Id", "dbo.DashboardVMs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "DashboardVM_Id", "dbo.DashboardVMs");
            DropIndex("dbo.Activities", new[] { "DashboardVM_Id" });
            DropColumn("dbo.Activities", "DashboardVM_Id");
            DropTable("dbo.DashboardVMs");
        }
    }
}
