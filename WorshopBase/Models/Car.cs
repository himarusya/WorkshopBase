using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Car
    {
        public int carID { get; set; }
        public int ownerID { get; set; }
        public string model { get; set; }
        public int vis { get; set; }
        public string colour { get; set; }
        public string stateNumber { get; set; }
        public int yearOfIssue { get; set; }
        public int bodyNumber { get; set; }
        public int engineNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Owner Owner { get; set; }
    }
}
