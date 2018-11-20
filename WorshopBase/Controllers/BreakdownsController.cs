using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels;
using WorshopBase.ViewModels.BreakdownsViewModel;

namespace WorshopBase.Controllers
{
    [Authorize]
    public class BreakdownsController : Controller
    {
        WorkshopContext db;

        public BreakdownsController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            List<BreakdownViewModel> list = new List<BreakdownViewModel>();
            var breakdowns = db.Breakdowns.Include("Part").Include("Worker").Include(x => x.Order).ThenInclude(x => x.Car).ToList();
            foreach (var breakdown in breakdowns)
            {
                list.Add(new BreakdownViewModel
                {
                    Id = breakdown.breakdownID,
                    partName = breakdown.Part.partName,
                    fioWorker = breakdown.Worker.fioWorker,
                    stateNumber = breakdown.Order.Car.stateNumber
                });
            }
            IQueryable<BreakdownViewModel> filterList = list.AsQueryable();
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            BreakdownsListViewModel model = new BreakdownsListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Breakdowns = items
            };
            return View(model);
        }

        public IActionResult Create()
        {
            Dictionary<string, int> parts = new Dictionary<string, int>();
            Dictionary<string, int> workers = new Dictionary<string, int>();
            Dictionary<string, int> orders = new Dictionary<string, int>();
            foreach(var item in db.Parts)
            {
                parts.Add(item.partName, item.partID);
            }
            foreach(var item in db.Workers)
            {
                workers.Add(item.fioWorker, item.workerID);
            }
            foreach (var item in db.Orders.Include("Car"))
            {
                orders.Add(item.Car.stateNumber.ToString() + " " + item.dateReceipt.ToString("dd.MM.yyyy"), 
                    item.orderID);
            }
            ViewBag.parts = parts;
            ViewBag.workers = workers;
            ViewBag.orders = orders;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Breakdown model)
        {
            if (ModelState.IsValid)
            {
                Breakdown breakdown = new Breakdown()
                {
                    orderID = model.orderID,
                    partID = model.partID,
                    workerID = model.workerID
                };
                await db.Breakdowns.AddAsync(breakdown);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id != null)
            {
                Breakdown breakdown = await db.Breakdowns.FirstOrDefaultAsync(t => t.breakdownID == id);
                if (breakdown == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                    };
                    return View("Error", error);
                }
                Dictionary<string, int> parts = new Dictionary<string, int>();
                Dictionary<string, int> workers = new Dictionary<string, int>();
                Dictionary<string, int> orders = new Dictionary<string, int>();
                foreach (var item in db.Parts)
                {
                    parts.Add(item.partName, item.partID);
                }
                foreach (var item in db.Workers)
                {
                    workers.Add(item.fioWorker, item.workerID);
                }
                foreach (var item in db.Orders.Include("Car"))
                {
                    orders.Add(item.Car.stateNumber.ToString() + " " 
                        + item.dateReceipt.ToString("dd.MM.yyyy"), item.orderID);
                }
                EditBreakdownViewModel model = new EditBreakdownViewModel
                {
                    Id = breakdown.breakdownID,
                    partID = breakdown.Part.partID,
                    workerID = breakdown.Worker.workerID,
                    orderID = breakdown.Order.orderID
                };
                ViewBag.parts = parts;
                ViewBag.workers = workers;
                ViewBag.orders = orders;
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public async Task<IActionResult> Edit(EditBreakdownViewModel model)
        {
            Breakdown breakdown = await db.Breakdowns.Include("Part").Include("Worker").Include(x => x.Order).ThenInclude(x => x.Car).FirstOrDefaultAsync(t => t.breakdownID == model.Id);
            if (ModelState.IsValid)
            {
                if (breakdown == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
            }
            Dictionary<string, int> parts = new Dictionary<string, int>();
            Dictionary<string, int> workers = new Dictionary<string, int>();
            Dictionary<string, int> orders = new Dictionary<string, int>();
            foreach (var item in db.Parts)
            {
                parts.Add(item.partName, item.partID);
            }
            foreach (var item in db.Workers)
            {
                workers.Add(item.fioWorker, item.workerID);
            }
            foreach (var item in db.Orders.Include("Car"))
            {
                orders.Add(item.Car.stateNumber.ToString() + " "
                    + item.dateReceipt.ToString("dd.MM.yyyy"), item.orderID);
            }
            ViewBag.parts = parts;
            ViewBag.workers = workers;
            ViewBag.orders = orders;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Breakdown breakdown = await db.Breakdowns.FirstOrDefaultAsync(t => t.breakdownID == id);
            db.Breakdowns.Remove(breakdown);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "Доступ только для администратора!"
            };
            return View("Error", error);
        }
    }
}
