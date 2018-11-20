using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels.OrdersViewModels
{
    public class OrderFilterViewModel
    {
        public string Car { get; set; }
        public SelectList Workers { get; set; }
        public int? SelectedWorker { get; set; }
        [DataType(DataType.Date)]
        public DateTime _date1 { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? _date2 { get; set; } = DateTime.Now;
        public string _selectedType { get; set; }

        public OrderFilterViewModel()
        {

        }

        public OrderFilterViewModel(string stateNumber, List<Worker> workers, int? worker,
            DateTime date1, DateTime? date2, string selectedType)
        {
            Car = stateNumber;
            if (workers.Where(p => p.fioWorker == "Все").Count() == 0)
                workers.Insert(0, new Worker { fioWorker = "Все", workerID = -1 });
            Workers = new SelectList(workers, "workerID", "fioWorker", worker);
            _date1 = date1;
            _date2 = date2;
            _selectedType = selectedType;
        }
    }
}
