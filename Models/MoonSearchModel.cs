using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Models
{
    public class MoonSearchModel
    {
        [Display(Name = "System")]
        public string SystemName { get; set; }
        [Display(Name = "Ore")]
        public string OreName { get; set; }
        [Display(Name = "Ore Tier")]
        public string OreTier { get; set; }
        [Display(Name = "Percentage (0~100)")]
        [Range(0, 100)]
        public int Percentage { get; set; }

        public List<string> OreList = new List<string>();
        public List<string> OreTierList = new List<string>();
    }
}
