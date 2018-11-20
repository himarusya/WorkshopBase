using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.ViewModels.OrdersViewModels;
using WorshopBase.Models;

namespace WorshopBase.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Breakdown> Breakdowns { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Owner> Owners { get; set; }
        public IEnumerable<Part> Parts { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public List<Worker> Workers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
