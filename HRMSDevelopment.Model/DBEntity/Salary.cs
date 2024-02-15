using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class Salary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalaryId { get; set; }

        [ForeignKey("Employee")]
        [Required(ErrorMessage = "Employee Id is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Basic Salary is required")]
        public decimal BasicSalary { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<SalaryRevision>? SalaryRevisions { get; set; }
        public virtual ICollection<Deduction>? Deductions { get; set;}
        public virtual ICollection<Bonus>? Bonus { get; set; }
        public Salary()
        {
            SalaryRevisions = new List<SalaryRevision>();
            Bonus = new List<Bonus>();
            Deductions = new List<Deduction>();
        }
    }
}
