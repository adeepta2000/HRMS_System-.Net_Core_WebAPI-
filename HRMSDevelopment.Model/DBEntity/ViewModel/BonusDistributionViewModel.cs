using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class BonusDistributionViewModel
    {
        public int bonus_criteriaId { get; set; }
        public int employee_id { get; set; }
        public decimal bonus_amount { get; set; }
        public DateTime distribution_date { get; set; }
    }
}
