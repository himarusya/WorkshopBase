using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Owner
    {
        public int ownerID { get; set; }
        public int driverLicense { get; set; }
        public string fioOwner { get; set; }
        public string adress { get; set; }
        public int phone { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
