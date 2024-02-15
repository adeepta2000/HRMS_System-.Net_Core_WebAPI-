using HRMSDevelopment.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        public string? MobileNumber { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string? Position { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "Employment Status is required")]
        public EmploymentStatus EmploymentStatus { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<PayrollRecord> PayrollRecords { get; set; }
        public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; }
        public virtual ICollection<BonusDistribution> BonusDistributions { get; set; }

        public Employee()
        {
            Salaries = new List<Salary>();
            PayrollRecords = new List<PayrollRecord>();
            EmploymentHistories = new List<EmploymentHistory>();
            BonusDistributions = new List<BonusDistribution>();
        }
    }
}
