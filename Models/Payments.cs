using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace LowCostHousing.Models
{
    [Table("Payments")]
    public class Payments
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("ClientRegistrationId")]
        public int ClientRegistrationId { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public double AmountPaid { get; set; }

        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaidOn { get; set; }

        public int ReceivedBy { get; set; }

        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public virtual ProjectMaster ProjectMaster { get; set; }
        public virtual ClientRegistration ClientRegistration { get; set; }
        public string FileName { get; set; }
        [StringLength(int.MaxValue)]
        public string FilePath { get; set; }
        [StringLength(int.MaxValue)]
        public string Description { get; set; }
    }
}
