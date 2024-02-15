using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class BonusCriteriaViewModel
    {
        public string? criteria_name { get; set; }
        public string? criteria_description { get; set; }
        public DateTime valid_from { get; set; }
        public Decimal minimum_bonusAmount { get; set; }
        public Decimal maximum_bonusAmount { get; set; }
    }
}
