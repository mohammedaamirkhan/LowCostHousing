using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace LowCostHousing.Models
{
    public class GetPaymentDetails
    {
        [Key]
        public int ClientRegistrationId { get; set; }
        public string ClientGivenName { get; set; }
        public string ClientFamilyName { get; set; }
        public string ProjectName { get; set; }
        public int MobileNumber { get; set; }
        public int LotNo { get; set; }
        public int ReferenceNumber { get; set; }
        public int ProjectMasterId { get; set; }

    }
}
