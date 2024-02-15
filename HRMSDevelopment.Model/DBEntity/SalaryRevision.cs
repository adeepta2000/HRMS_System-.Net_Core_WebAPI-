using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class SalaryRevision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RevisionId { get; set; }

        [ForeignKey("Salary")]
        [Required(ErrorMessage = "Salary Id is required")]
        public int SalaryId { get; set; }

        [Required(ErrorMessage = "Revised Salary is required")]
        public decimal RevisedSalary { get; set; }

        [Required(ErrorMessage = "Salary Revision Date is required")]
        public DateTime RevisionDate { get; set; }
        public virtual Salary? Salary { get; set; }
    }
}
