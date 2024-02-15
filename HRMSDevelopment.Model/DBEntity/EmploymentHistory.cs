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
    public class EmploymentHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmploymentHistoryId { get; set; }

        [ForeignKey("Employee")]
        [Required(ErrorMessage = "Employee id is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        public string? JobTitle { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "Employment Action is required")]
        public EmploymentAction EmploymentAction { get; set; }
        public DateTime ActionDate { get; set; }
        public string? Comments { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
