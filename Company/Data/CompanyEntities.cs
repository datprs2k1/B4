using System.Data.Entity;

namespace Company.Data
{
    public class CompanyEntities : DbContext
    {
        public CompanyEntities()
            : base("name=CompanyEntities")
        {
        }

        #region DbSet
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOptional(e => e.parentDepartment)
                .WithMany(e => e.childDepartments)
                .HasForeignKey(e => e.parent);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.department_id);

            modelBuilder.Entity<Salary>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Salary)
                .HasForeignKey(e => e.salary_id);

            modelBuilder.Entity<Salary>()
                .Property(e => e.coefficient)
                .HasPrecision(4, 2);
        }
    }
}