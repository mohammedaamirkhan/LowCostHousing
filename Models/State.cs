using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LowCostHousing.Models
{
    [Table("State")]
    public class State
    {
        [Key]
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
