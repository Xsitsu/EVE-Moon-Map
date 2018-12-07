using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Models
{
    public class MoonAddModel
    {
        [Required]
        [Display(Name = "Paste Contents of Moon Probe Scan Here")]
        public string Data { get; set; }
    }
}
