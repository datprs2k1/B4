namespace Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPhoneToEmployeeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.employees", "phone", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.employees", "phone");
        }
    }
}
