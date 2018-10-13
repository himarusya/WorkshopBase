using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels
{
    public class OwnerViewModel
    {
        public int Id { get; set; }
        public int driverLicense { get; set; }
        public string fioOwner { get; set; }
        public string adress { get; set; }
        public int phone { get; set; }
    }
}
