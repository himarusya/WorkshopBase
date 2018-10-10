using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public int carID { get; set; }
        public DateTime dateReceipt { get; set; }
        public DateTime dateCompletion { get; set; }
        public int workerID { get; set; }

        public ICollection<Breakdown> Breakdowns { get; set; }
        public Car Car { get; set; }
        public Worker Worker { get; set; }
    }
}
