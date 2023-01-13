namespace Company.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.employees",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 50),
                    dob = c.DateTime(nullable: false),
                    address = c.String(nullable: false, maxLength: 50),
                    gender = c.Byte(nullable: false),
                    startdate = c.DateTime(nullable: false),
                    department_id = c.Int(nullable: false),
                    salary_id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.departments", t => t.department_id, cascadeDelete: true)
                .ForeignKey("dbo.salaries", t => t.salary_id, cascadeDelete: true)
                .Index(t => t.id, unique: true)
                .Index(t => t.department_id)
                .Index(t => t.salary_id);

            CreateTable(
                "dbo.departments",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    code = c.String(nullable: false, maxLength: 10),
                    name = c.String(nullable: false, maxLength: 50),
                    description = c.String(nullable: false, maxLength: 50),
                    parent = c.Int(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.departments", t => t.parent)
                .Index(t => t.id, unique: true)
                .Index(t => t.parent);

            CreateTable(
                "dbo.salaries",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 50),
                    coefficient = c.Decimal(nullable: false, precision: 4, scale: 2),
                })
                .PrimaryKey(t => t.id)
                .Index(t => t.id, unique: true);

            CreateTable(
                "dbo.attendances",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    employee_id = c.Int(nullable: false),
                    status = c.Int(nullable: false),
                    created_at = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.employees", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.id, unique: true)
                .Index(t => t.employee_id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.attendances", "employee_id", "dbo.employees");
            DropForeignKey("dbo.employees", "salary_id", "dbo.salaries");
            DropForeignKey("dbo.departments", "parent", "dbo.departments");
            DropForeignKey("dbo.employees", "department_id", "dbo.departments");
            DropIndex("dbo.salaries", new[] { "id" });
            DropIndex("dbo.departments", new[] { "parent" });
            DropIndex("dbo.departments", new[] { "id" });
            DropIndex("dbo.employees", new[] { "salary_id" });
            DropIndex("dbo.employees", new[] { "department_id" });
            DropIndex("dbo.employees", new[] { "id" });
            DropIndex("dbo.attendances", new[] { "employee_id" });
            DropIndex("dbo.attendances", new[] { "id" });
            DropTable("dbo.salaries");
            DropTable("dbo.departments");
            DropTable("dbo.employees");
            DropTable("dbo.attendances");
        }
    }
}
