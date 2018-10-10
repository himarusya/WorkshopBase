using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Part
    {
        public int partID { get; set; }
        public string partName { get; set; }
        public decimal price { get; set; }
        public string descriptionPart { get; set; }

        public ICollection<Breakdown> Breakdowns { get; set; }
    }
}
