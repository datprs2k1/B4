using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Data
{
    [Table("attendances")]
    public partial class Attendance
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int employee_id { get; set; }
        [Required]
        [DefaultValue(0)]
        public int status { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
