using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class DeductionViewModel
    {
        public int salary_id { get; set; }
        public decimal deduction_ammount { get; set; }
        public DateTime deduction_date { get; set; }
    }
}
