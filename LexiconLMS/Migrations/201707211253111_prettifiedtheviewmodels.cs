namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prettifiedtheviewmodels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Documents", "Filename", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Filename", c => c.String());
            AlterColumn("dbo.Activities", "Name", c => c.String());
        }
    }
}
