namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPhoneNumberHandlingToTeachersAndStudents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateTeacherAccountVMs", "PhoneNumber", c => c.String());
            AddColumn("dbo.CreateTeacherAccountVMs", "ReturnToIndex", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateTeacherAccountVMs", "ReturnToIndex");
            DropColumn("dbo.CreateTeacherAccountVMs", "PhoneNumber");
        }
    }
}
