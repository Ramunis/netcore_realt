using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCLab2.Models
{
    public class buyboard
    {
        public int ID { get; set; }

        public DateTime DZ { get; set; }

        public string Clientf { get; set; }

        public string Realtf { get; set; }

        public string District { get; set; }

        public string Servic { get; set; }

        public int Sq { get; set; }

        public DateTime DS { get; set; }

        public int Term { get; set; }

        public decimal Price { get; set; }

        public string Pay { get; set; }

        public string Repair { get; set; }
    }
}
