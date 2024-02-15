using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class BonusCriteria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BonusCriteriaId { get; set; }

        [Required(ErrorMessage = "Bonus Criteria Name is required")]
        public string? CriteriaName { get; set; }
        public string? CriteriaDescription { get; set; }

        [Required(ErrorMessage = "Bonus Validation Stating Date is required")]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Minimum bonus ammount is required")]
        public Decimal MinimumBonusAmount { get; set; }

        [Required(ErrorMessage = "Maximum bonus ammount is required")]
        public Decimal MaximumBonusAmount { get; set; }
        public virtual ICollection<BonusDistribution> BonusDistributions { get; set; }
        public BonusCriteria()
        {
            BonusDistributions = new List<BonusDistribution>();
        }

    }
}
