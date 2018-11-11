using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels.OrdersViewModels;
using WorshopBase.ViewModels;

namespace WorshopBase.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        WorkshopContext db;

        public OrdersController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Orders.ToList());
        }
    }
}