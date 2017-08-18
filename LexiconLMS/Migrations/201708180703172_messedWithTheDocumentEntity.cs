namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messedWithTheDocumentEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "PurposeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Documents", "PurposeId");
            AddForeignKey("dbo.Documents", "PurposeId", "dbo.Purposes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "PurposeId", "dbo.Purposes");
            DropIndex("dbo.Documents", new[] { "PurposeId" });
            DropColumn("dbo.Documents", "PurposeId");
        }
    }
}
