using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;
using WorshopBase.ViewModels.OrdersViewModels;
using WorshopBase.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WorshopBase.Services
{
    public class WorkshopService
    {
        private WorkshopContext db;

        public WorkshopService(WorkshopContext context)
        {
            db = context;
        }

        public HomeViewModel GetHomeViewModel()
        {
            var cars = db.Cars.ToList();
            var workers = db.Workers.ToList();
            var orders = db.Orders.Include("Worker").Include(x => x.Car).ThenInclude(x =>
                x.Owner).Include(y => y.Breakdowns).ThenInclude(y => y.Part).ToList();

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Workers = workers,
                Cars = cars,
                Orders = orders
            };
            return homeViewModel;
        }
    }
}
