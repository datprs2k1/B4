namespace Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteColumnPhoneToEmployeeTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.employees", "phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.employees", "phone", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
