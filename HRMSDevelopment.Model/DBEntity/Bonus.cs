using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class Bonus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BonusId { get; set; }

        [ForeignKey("Salary")]
        [Required(ErrorMessage = "Salary Id is required")]
        public int SalaryId { get; set; }

        [Required(ErrorMessage = "Bonus Ammount is required")]
        public decimal BonusAmmount { get; set; }

        [Required(ErrorMessage = "Bonus Date is required")]
        public DateTime BonusDate { get; set; }
        public virtual Salary? Salary { get; set; }

    }
}
