using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Data
{
    [Table("employees")]
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
        }
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        public DateTime dob { get; set; }
        [Required]
        [StringLength(50)]
        public string address { get; set; }
        [Required]
        public byte gender { get; set; }
        [Required]
        public DateTime startdate { get; set; }
        public int department_id { get; set; }
        public int salary_id { get; set; }
        public virtual Salary Salary { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}
