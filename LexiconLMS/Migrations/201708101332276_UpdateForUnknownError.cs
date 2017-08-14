namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForUnknownError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DashboardVMs", "ModuleExists", c => c.Boolean(nullable: false));
            AddColumn("dbo.DashboardVMs", "ActivityExists", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DashboardVMs", "ActivityExists");
            DropColumn("dbo.DashboardVMs", "ModuleExists");
        }
    }
}
