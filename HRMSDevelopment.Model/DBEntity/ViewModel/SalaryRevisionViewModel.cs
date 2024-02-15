using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class SalaryRevisionViewModel
    {
        public int salary_id { get; set; }
        public decimal revised_salary { get; set; }
        public DateTime revision_date { get; set; }
    }
}
