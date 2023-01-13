namespace Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendancesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.attendances",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        employee_id = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.employees", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.id, unique: true)
                .Index(t => t.employee_id);
            
            CreateIndex("dbo.employees", "id", unique: true);
            CreateIndex("dbo.departments", "id", unique: true);
            CreateIndex("dbo.salaries", "id", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.attendances", "employee_id", "dbo.employees");
            DropIndex("dbo.salaries", new[] { "id" });
            DropIndex("dbo.departments", new[] { "id" });
            DropIndex("dbo.employees", new[] { "id" });
            DropIndex("dbo.attendances", new[] { "employee_id" });
            DropIndex("dbo.attendances", new[] { "id" });
            DropTable("dbo.attendances");
        }
    }
}
