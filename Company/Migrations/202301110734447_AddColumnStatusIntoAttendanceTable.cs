namespace Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnStatusIntoAttendanceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.attendances", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.attendances", "status");
        }
    }
}
