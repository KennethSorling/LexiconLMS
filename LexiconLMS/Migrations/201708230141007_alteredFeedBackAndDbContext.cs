namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredFeedBackAndDbContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FeedBacks", "Comments", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FeedBacks", "Comments", c => c.String());
        }
    }
}
