using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels.CarsViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string fioOwner { get; set; }
        public string model { get; set; }
        public int vis { get; set; }
        public string colour { get; set; }
        public string stateNumber { get; set; }
        public int yearOfIssue { get; set; }
        public int bodyNumber { get; set; }
        public int engineNumber { get; set; }
    }
}
