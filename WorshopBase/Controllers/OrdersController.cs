using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorshopBase.Models;
using WorshopBase.ViewModels.OrdersViewModels;
using WorshopBase.ViewModels;
using WorshopBase.ViewModels.BreakdownsViewModel;
using WorshopBase.Services;
using WorshopBase.Infrastructure.Filters;
using WorshopBase.Infrastructure;

namespace WorshopBase.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        WorkshopContext db;
        private IMemoryCache memoryCache;
        WorkshopService service;

        public OrdersController(WorkshopContext context, IMemoryCache memoryCache, 
            WorkshopService service)
        {
            db = context;
            this.memoryCache = memoryCache;
            this.service = service;
        }

        public IActionResult Index(int page = 1)
        {
            try
            {
                int pageSize = 5;
                HomeViewModel entryCache = memoryCache.Get<HomeViewModel>("Workshop");
                List<OrderViewModel> list = new List<OrderViewModel>();
                var orders = entryCache.Orders;
                var sessionOrder = HttpContext.Session.Get("OrderFilters");
                OrderFilterViewModel filterOrder = null;
                if (sessionOrder != null)
                {
                    filterOrder = Transformations.DictionaryToObject<OrderFilterViewModel>(sessionOrder);
                }
                foreach (var order in orders)
                {
                    list.Add(new OrderViewModel
                    {
                        Id = order.orderID,
                        dateCompletion = order.dateCompletion,
                        dateReceipt = order.dateReceipt,
                        fioOwner = order.Car.Owner.fioOwner,
                        fioWorker = order.Worker.fioWorker,
                        stateNumber = order.Car.stateNumber,
                        workerID = order.Worker.workerID,
                        price = order.Breakdowns.Sum(p => p.Part.price)
                    });
                }
                IQueryable<OrderViewModel> filterList = list.AsQueryable();
                if (filterOrder != null)
                {
                    if (!string.IsNullOrEmpty(filterOrder.Car))
                    {
                        filterList = filterList.Where(p => p.stateNumber == filterOrder.Car);
                    }
                    if (filterOrder.SelectedWorker != null && filterOrder.SelectedWorker != -1)
                    {
                        filterList = filterList.Where(p => p.workerID == filterOrder.SelectedWorker);
                    }
                    switch (filterOrder._selectedType)
                    {
                        case "Отремонтированные":
                        {
                            if (filterOrder._date1 != null)
                            {
                                filterList = filterList.Where(p => p.dateCompletion != null &&
                                    p.dateCompletion >= filterOrder._date1);
                            }
                            if (filterOrder._date2 != null)
                            {
                                filterList = filterList.Where(p => p.dateCompletion != null &&
                                    p.dateCompletion <= filterOrder._date2);
                            }
                            break;
                        }
                        case "Поступившие":
                        {
                            if (filterOrder._date1 != null)
                            {
                                filterList = filterList.Where(p => p.dateReceipt >= filterOrder._date1);
                            }
                            if (filterOrder._date2 != null)
                            {
                                filterList = filterList.Where(p => p.dateReceipt <= filterOrder._date2);
                            }
                            break;
                        }
                    }

                }
                var count = filterList.Count();
                var items = filterList.Skip((page - 1) * pageSize).
                    Take(pageSize).ToList();
                if (filterOrder != null)
                    filterOrder.Workers = new SelectList(entryCache.Workers, "workerID", "fioWorker");
                OrdersListViewModel model = new OrdersListViewModel
                {
                    PageViewModel = new PageViewModel(count, page, pageSize),
                    OrderFilterViewModel = filterOrder == null ? new OrderFilterViewModel(null, entryCache.Workers, null, DateTime.Now, DateTime.Now, null) : filterOrder,
                    Orders = items
                };
                return View(model);
            }
            catch (Exception ex)
            {

            }
            return View("Error");
        }

        [HttpPost]
        [SetToSession("OrderFilters")]
        public IActionResult Index(OrderFilterViewModel filterOrder)
        {
            int pageSize = 5;
            HomeViewModel entryCache = memoryCache.Get<HomeViewModel>("Workshop");
            List<OrderViewModel> list = new List<OrderViewModel>();
            var orders = entryCache.Orders;
            foreach (var order in orders)
            {
                list.Add(new OrderViewModel
                {
                    Id = order.orderID,
                    dateCompletion = order.dateCompletion,
                    dateReceipt = order.dateReceipt,
                    fioOwner = order.Car.Owner.fioOwner,
                    fioWorker = order.Worker.fioWorker,
                    stateNumber = order.Car.stateNumber,
                    workerID = order.Worker.workerID,
                    price = order.Breakdowns.Sum(p => p.Part.price)
                });
            }
            IQueryable<OrderViewModel> filterList = list.AsQueryable();
            if (filterOrder.Car != null)
            {
                filterList = filterList.Where(p => p.stateNumber == filterOrder.Car);
            }
            if (filterOrder.SelectedWorker != null && filterOrder.SelectedWorker != -1)
            {
                filterList = filterList.Where(p => p.workerID == filterOrder.SelectedWorker);
            }
            switch(filterOrder._selectedType)
            {
                case "Отремонтированные":
                {
                    if (filterOrder._date1 != null)
                    {
                        filterList = filterList.Where(p => p.dateCompletion != null && 
                            p.dateCompletion >= filterOrder._date1);
                    }
                    if(filterOrder._date2 != null)
                    {
                        filterList = filterList.Where(p => p.dateCompletion != null &&
                            p.dateCompletion <= filterOrder._date2);
                    }
                    break;
                }
                case "Поступившие":
                {
                    if (filterOrder._date1 != null)
                    {
                        filterList = filterList.Where(p => p.dateReceipt >= filterOrder._date1);
                    }
                    if (filterOrder._date2 != null)
                    {
                        filterList = filterList.Where(p => p.dateReceipt <= filterOrder._date2);
                    }
                    break;
                }
            }
            var count = filterList.Count();
            var items = filterList.Take(pageSize).ToList();
            OrdersListViewModel model = new OrdersListViewModel
            {
                PageViewModel = new PageViewModel(count, 1, pageSize),
                OrderFilterViewModel = new OrderFilterViewModel(filterOrder.Car, 
                    db.Workers.ToList(), filterOrder.SelectedWorker, filterOrder._date1, filterOrder._date2, filterOrder._selectedType),
                Orders = items
            };
            return View(model);
        }

        public IActionResult Create()
        {
            HomeViewModel entryCache = memoryCache.Get<HomeViewModel>("Workshop");
            Dictionary<string, int> cars = new Dictionary<string, int>();
            Dictionary<string, int> workers = new Dictionary<string, int>();
            foreach (var item in entryCache.Cars)
            {
                cars.Add(item.stateNumber, item.carID);
            }
            foreach(var item in entryCache.Workers)
            {
                workers.Add(item.fioWorker, item.workerID);
            }
            ViewBag.cars = cars;
            ViewBag.workers = workers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order model)
        {
            HomeViewModel entryCache = memoryCache.Get<HomeViewModel>("Workshop");
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    carID = model.carID,
                    dateReceipt = model.dateReceipt,
                    dateCompletion = model.dateCompletion,
                    workerID = model.workerID
                };
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                var workshop = service.GetHomeViewModel();
                memoryCache.Set("Workshop", workshop);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            HomeViewModel entryCache = memoryCache.Get<HomeViewModel>("Workshop");
            Dictionary<string, int> cars = new Dictionary<string, int>();
            Dictionary<string, int> workers = new Dictionary<string, int>();
            foreach (var item in entryCache.Cars)
            {
                cars.Add(item.stateNumber, item.carID);
            }
            foreach (var item in entryCache.Workers)
            {
                workers.Add(item.fioWorker, item.workerID);
            }
            var model = entryCache.Orders.FirstOrDefault(p => p.orderID == id);
            ViewBag.cars = cars;
            ViewBag.workers = workers;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order model)
        {
            try
            {
                HomeViewModel entryCache = memoryCache.Get<HomeViewModel>("Workshop");
                var order = db.Orders.FirstOrDefault(t => t.orderID == model.orderID);
                if (ModelState.IsValid)
                {
                    if (order == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! Прислана пустая модель"
                        };
                        return View("Error", error);
                    }
                    order.carID = model.carID;
                    order.dateReceipt = model.dateReceipt;
                    order.dateCompletion = model.dateCompletion;
                    order.workerID = model.workerID;
                    await db.SaveChangesAsync();
                    var workshop = service.GetHomeViewModel();
                    memoryCache.Set("Workshop", workshop);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Order оrder = await db.Orders.FirstOrDefaultAsync(t => t.orderID == id);
            db.Orders.Remove(оrder);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Breakdowns(int? id)
        {
            try
            {
                if (id != null)
                {
                    ViewBag.orderID = id;
                    List<BreakdownViewModel> list = new List<BreakdownViewModel>();
                    var breakdowns = db.Breakdowns.Include("Part").Include("Worker")
                        .Include(x => x.Order).ThenInclude(x => x.Car).ToList()
                        .Where(t => t.orderID == id);
                    foreach (var breakdown in breakdowns)
                    {
                        list.Add(new BreakdownViewModel
                        {
                            Id = breakdown.breakdownID,
                            partName = breakdown.Part.partName,
                            fioWorker = breakdown.Worker.fioWorker,
                            stateNumber = breakdown.Order.Car.stateNumber,
                            price = breakdown.Part.price
                        });
                    }
                    Order post = await db.Orders.Include(x => x.Breakdowns).ThenInclude(x => x.Order).FirstOrDefaultAsync(t => t.orderID == id);
                    if (post == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                    return View(list);
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                    return View("Error", error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение! {0} ", ex);
            }
            ViewBag.orderID = id;
            return View();
        }

        public IActionResult CreateBreakdown(int? orderID)
        {
            try
            {
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
                    orders.Add(item.Car.stateNumber.ToString() + " " + item.dateReceipt.ToString("dd.MM.yyyy"),
                        item.orderID);
                }
                ViewBag.parts = parts;
                ViewBag.workers = workers;
                ViewBag.orders = orders;
                ViewBag.orderID = orderID;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBreakdown(Breakdown model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Breakdown breakdown = new Breakdown()
                    {
                        partID = model.partID,
                        workerID = model.workerID,
                        orderID = model.orderID
                    };
                    await db.Breakdowns.AddAsync(breakdown);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Breakdowns", new { id = model.orderID });
                }
                catch(Exception ex)
                {

                }
            }
            ViewBag.orderID = model.orderID;
            return View(model);
        }

        public async Task<IActionResult> EditBreakdown(int? id)
        {
            if (id != null)
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
                    orders.Add(item.Car.stateNumber.ToString() + " " + item.dateReceipt.ToString("dd.MM.yyyy"),
                        item.orderID);
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

        [HttpPost]
        public async Task<IActionResult> EditBreakdown(EditBreakdownViewModel model)
        {
            try
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
                    breakdown.orderID = model.orderID;
                    breakdown.partID = model.partID;
                    breakdown.workerID = model.workerID;
                    db.SaveChanges();
                    return RedirectToAction("Breakdowns", new { id = model.orderID });
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
            catch(Exception ex)
            {
                return RedirectToAction("Account", "AccessDenied");
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteBreakdown(int? id)
        {
            Breakdown breakdown = await db.Breakdowns.FirstOrDefaultAsync(t => 
                t.breakdownID == id);
            int breakdownID = breakdown.orderID;
            db.Breakdowns.Remove(breakdown);
            await db.SaveChangesAsync();
            return RedirectToAction("Breakdowns", new { id = breakdownID });
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