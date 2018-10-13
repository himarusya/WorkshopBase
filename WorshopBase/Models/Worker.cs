using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Worker
    {
        public int workerID { get; set; }
        public string fioWorker { get; set; }
        public int? postID { get; set; }
        public DateTime dateOfEmployment { get; set; }
        public DateTime? dateOfDismissal { get; set; }
        public decimal salary { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Breakdown> Breakdowns { get; set; }
        public Post Post { get; set; }
    }
}
