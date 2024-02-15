using HRMSDevelopment.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class EmploymentHistoryViewModel
    {
        public int employee_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string? job_title { get; set; }
        public decimal salary { get; set; }
        public string? department { get; set; }
        public EmploymentAction employment_action { get; set; }
        public DateTime action_date { get; set; }
        public string? comments { get; set; }
    }
}
