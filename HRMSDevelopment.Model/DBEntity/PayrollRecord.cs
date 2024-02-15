using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class PayrollRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PayrollId { get; set; }

        [ForeignKey("Employee")]
        [Required(ErrorMessage = "Employee Id is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Gross Pay is required")]
        public decimal GrossPay { get; set; }

        [Required(ErrorMessage = "Total deduction is required")]
        public decimal TotalDeductions { get; set; }

        [Required(ErrorMessage = "Total bonus is required")]
        public decimal TotalBonuses { get; set; }

        [Required(ErrorMessage = "NetPay is required")]
        public decimal NetPay { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
