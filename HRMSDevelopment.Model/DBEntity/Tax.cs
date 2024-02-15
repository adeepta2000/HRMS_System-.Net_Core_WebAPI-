using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSDevelopment.Model.DBEntity
{
    public class Tax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "Tax Lower Bound is required")]
        public decimal LowerBound { get; set; }

        [Required(ErrorMessage = "Tax Upper Bound is required")]
        public decimal UpperBound { get; set; }

        [Required(ErrorMessage = "Tax rate is required")]
        public decimal Rate { get; set; }
    }
}
