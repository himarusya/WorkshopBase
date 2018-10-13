using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels;

namespace WorshopBase.Controllers
{
    public class OrdersController : Controller
    {
        WorkshopContext db;

        public OrdersController(WorkshopContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            var ordersList = db.Orders.Include("Worker").Include(x => x.Car).ThenInclude(x => x.Owner).ToList();
            List<OrderViewModel> list = new List<OrderViewModel>();
            foreach (var item in ordersList)
            {
                list.Add(new OrderViewModel
                {
                    Id = item.carID,
                    dateCompletion = item.dateCompletion,
                    dateReceipt = item.dateReceipt,
                    fioOwner = item.Car.Owner.fioOwner,
                    fioWorker = item.Worker.fioWorker,
                    model = item.Car.model,
                    stateNumber = item.Car.stateNumber
                });
            }
            return View(list);
        }
    }
}