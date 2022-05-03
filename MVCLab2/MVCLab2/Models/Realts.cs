using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCLab2.Models
{
    public class Realts
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string PW { get; set; }

        public string F { get; set; }

        public string I { get; set; }

        public string O { get; set; }

        public DateTime DR { get; set; }

        public decimal Salar { get; set; }

        public string City { get; set; }

        public string Adres { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
