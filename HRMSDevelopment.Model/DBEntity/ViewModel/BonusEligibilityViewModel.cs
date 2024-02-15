using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class BonusEligibilityViewModel
    {
        public int employee_id { get; set; }
        public int bonus_criteriaid { get; set; }
        public string? notes { get; set; }
    }
}
