namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dontKnowWhyIHaveToDoThis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EditTeacherAccountVMs", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EditTeacherAccountVMs", "PhoneNumber");
        }
    }
}
