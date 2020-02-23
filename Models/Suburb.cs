using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LowCostHousing.Models
{
    [Table("Suburb")]
    public class Suburb
    {
        [Key]
        public int SuburbID { get; set; }
        public string SuburbName { get; set; }
        [ForeignKey("StateID")]
        public int StateID { get; set; }
    }
}
