using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF11207_TP2_MarianePouliot_NathanStOnge.Models
{
    internal class Prediction
    {
        public int Id { get; set; }
        public int Quality { get; set; }
        public Setting? Setting { get; set; }
        public int SettingId { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public string? Date { get; set; }
        [NotMapped]
        public int ValueK { get; set; }
        [NotMapped]
        public string? SortAlgo { get; set; }
        [NotMapped]
        public float Alcool { get; set; }
        [NotMapped]
        public float Sulfate { get; set; }
        [NotMapped]
        public float AcidCitric { get; set; }
        [NotMapped]
        public float AcidVolatil { get; set; }



    }
}
