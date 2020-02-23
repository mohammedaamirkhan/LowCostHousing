using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LowCostHousing.Models
{
    [Table("PaymentPlanAmount")]
    public class PaymentPlanAmount
    {
        [Key]
        public int PaymentPlanAmountId { get; set; }

        public int ClientRegistrationId { get; set; }

        public int PaymentPlanId { get; set; }

        public double Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public int Dates { get; set; }
    }
}
