using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class Deduction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeductionId { get; set; }

        [ForeignKey("Salary")]
        [Required(ErrorMessage = "Salary Id is required")]
        public int SalaryId { get; set; }

        [Required(ErrorMessage = "Deduction Ammount is required")]
        public decimal DeductionAmmount { get; set; }

        [Required(ErrorMessage = "Deduction Date is required")]
        public DateTime DeductionDate { get; set; }
        public virtual Salary? Salary { get; set; }
    }
}
