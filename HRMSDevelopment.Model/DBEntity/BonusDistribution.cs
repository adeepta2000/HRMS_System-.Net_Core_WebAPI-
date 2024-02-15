using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class BonusDistribution
    {
        public int BonusDistributionId { get; set; }

        [ForeignKey("BonusCriteria")]
        public int BonusCriteriaId { get; set; }

        [ForeignKey(" Employee")]
        public int EmployeeId { get; set; }
        public decimal BonusAmount { get; set; }
        public DateTime DistributionDate { get; set; }
        public virtual BonusCriteria? BonusCriteria { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
