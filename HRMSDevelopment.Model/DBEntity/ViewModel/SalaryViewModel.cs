using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class SalaryViewModel
    {
        public int employee_id { get; set; }
        public decimal basic_salary { get; set; }
    }
}
