using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.WorkersViewModels
{
    public class CreateWorkerViewModel
    {
        [Required(ErrorMessage = "FIO is required field")]
        public string fioWorker { get; set; }
        public int postID { get; set; }
        public string postName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of employment is required field")]
        public DateTime dateOfEmployment { get; set; }
        [DataType(DataType.Date)]
        public DateTime? dateOfDismissal { get; set; }
        [Required(ErrorMessage = "Salary is required field")]
        public decimal salary { get; set; }
    }
}
