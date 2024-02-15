using HRMSDevelopment.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity.ViewModel
{
    public class EmployeeViewModel
    {
        public string? name { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? mobile_number { get; set; }
        public string? position { get; set; }
        public string? department { get; set; }
        public string? account_number { get; set; }
        public EmploymentStatus employment_status { get; set; }
        public string? password { get; set; }
    }
}
