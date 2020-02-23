using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LowCostHousing.Models
{
    [Table("ClientRegistration")]
    public class ClientRegistration
    {
        [Key]
        public int ClientRegistrationId { get; set; }
        public bool TypeOfOwnership { get; set; }
        [Required]
        public string ClientGivenName { get; set; }
        [Required]
        public string ClientFamilyName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        public int HomePhoneNumber { get; set; }
        [Required]
        public int MobileNumber { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public int StateID { get; set; }
        public int SuburbID { get; set; }
        public int PostCode { get; set; }
        public string EmploymentType { get; set; }
        public string EmployerName { get; set; }
        public string PositionTitle { get; set; }
        public string NomineeGivenName { get; set; }
        public string NomineeFamilyName { get; set; }
        public string NomineePassportNumber { get; set; }
        public string NomineeMobileNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [ForeignKey("ProjectMasterId")]
        public int ProjectMasterId { get; set; }
        public virtual ProjectMaster ProjectMaster { get; set; }
        [Required]
        public int LotNo { get; set; }
        [Required]
        public int ReferenceNumber { get; set; }

        public string UserId { get; set; }
    }
}
