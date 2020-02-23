using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LowCostHousing.Models
{
    [Table("PaymentPlan")]
    public class PaymentPlan
    {
        [Key]
        public int PaymentPlanId { get; set; }
        public int ClientRegistrationId { get; set; }
        public int ProjectMasterId { get; set; }
        public double LandCost { get; set; }
        public double DevelopmentCost { get; set; }
        public double TotalCost { get; set; }
        public double HoldingDeposit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CreatedBy { get; set; }

        //public List<PaymentPlanAmount> PaymentPlanAmount { get; set; }
    }
}
