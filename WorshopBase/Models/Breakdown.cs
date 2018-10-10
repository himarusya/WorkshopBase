using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Breakdown
    {
        public int breakdownID { get; set; }
        public int orderID { get; set; }
        public int partID { get; set; }
        public int workerID { get; set; }

        public Worker Worker { get; set; }
        public Order Order { get; set; }
        public Part Part { get; set; }
    }
}
