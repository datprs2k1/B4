using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Data
{
    [Table("departments")]
    public partial class Department
    {
        public Department()
        {
            childDepartments = new HashSet<Department>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(10)]
        public string code { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string description { get; set; }
        public int? parent { get; set; }
        public virtual Department parentDepartment { get; set; }
        public virtual ICollection<Department> childDepartments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
