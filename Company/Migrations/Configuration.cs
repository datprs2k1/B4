namespace Company.Migrations
{
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Company.Data.CompanyEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Company.Data.CompanyEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.            
            if (!context.Departments.Any())
            {
                context.Departments.AddOrUpdate(x => x.id,
                    new Data.Department { id = 1, code = "Area 1", name = "Area 1", description = "Area 1" },
                    new Data.Department { id = 2, code = "Area 2", name = "Area 2", description = "Area 2" },
                    new Data.Department { id = 3, code = "Area 3", name = "Area 3", description = "Area 3" },
                    new Data.Department { id = 4, code = "Depart 1", name = "Depart 1", description = "Depart 1", parent = 1 },
                    new Data.Department { id = 5, code = "Depart 2", name = "Depart 2", description = "Depart 2", parent = 1 },
                    new Data.Department { id = 6, code = "Depart 3", name = "Depart 3", description = "Depart 3", parent = 1 },
                    new Data.Department { id = 7, code = "Room 1", name = "Room 1", description = "Room 1", parent = 4 },
                    new Data.Department { id = 8, code = "Room 2", name = "Room 2", description = "Room 2", parent = 4 }
                );
            }

            if (!context.Salaries.Any())
            {
                context.Salaries.AddOrUpdate(x => x.id,
                    new Data.Salary { id = 1, name = "Grade 1", coefficient = 5 },
                    new Data.Salary { id = 2, name = "Grade 2", coefficient = 10 }
                );
            }


            if (!context.Employees.Any())
            {
                context.Employees.AddOrUpdate(x => x.id,
                    new Data.Employee { id = 1, name = "Mr.A", gender = 1, dob = System.DateTime.Today, address = "A", startdate = System.DateTime.Today, department_id = 7, salary_id = 1 },
                    new Data.Employee { id = 2, name = "Mr.B", gender = 1, dob = System.DateTime.Today, address = "B", startdate = System.DateTime.Today, department_id = 7, salary_id = 1 }
                );
            }

            if (!context.Attendances.Any())
            {
                context.Attendances.AddOrUpdate(x => x.id,
                    new Data.Attendance { id = 1, employee_id = 1, status = 1, created_at = System.DateTime.Today },
                    new Data.Attendance { id = 2, employee_id = 2, status = 1, created_at = System.DateTime.Today }
                );
            }
        }
    }
}
