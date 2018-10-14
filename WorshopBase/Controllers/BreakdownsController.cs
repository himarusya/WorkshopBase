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
    public class BreakdownsController : Controller
    {
        WorkshopContext db;

        public BreakdownsController(WorkshopContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            var breakdownsList = db.Breakdowns.Include("Part").Include("Worker").ToList();
            List<BreakdownViewModel> list = new List<BreakdownViewModel>();
            foreach (var item in breakdownsList)
            {
                list.Add(new BreakdownViewModel
                {
                    Id = item.breakdownID,
                    partName = item.Part.partName,
                    fioOwner = item.Worker.fioWorker
                });
            }
            return View(list);
        }
    }
}
