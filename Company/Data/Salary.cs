using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Data
{
    [Table("salaries")]
    public partial class Salary
    {
        public Salary()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [Required]
        public decimal coefficient { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
