using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.WorkersViewModels
{
    public class EditWorkerViewModel
    {
        public int Id { get; set; }
        public string fioWorker { get; set; }
        public string postName { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateOfEmployment { get; set; }
        [DataType(DataType.Date)]
        public DateTime? dateOfDismissal { get; set; }
        public decimal salary { get; set; }
    }
}
