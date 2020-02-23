using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LowCostHousing.Models
{
    [Table("ProjectMaster")]
    public class ProjectMaster
    {
        [Key]
        public int ProjectMasterId { get; set; }

        [Required(ErrorMessage ="Unit No is required")]
        public int UnitNo { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public int ReferenceNumber { get; set; }

        [Required]
        public string ProjectAddress { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public int BSB { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
