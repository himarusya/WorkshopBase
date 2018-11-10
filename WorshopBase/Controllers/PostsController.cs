using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels.PostsViewModels;
using WorshopBase.ViewModels.WorkersViewModels;
using WorshopBase.ViewModels;

namespace WorshopBase.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        WorkshopContext db;

        public PostsController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Posts.Count(p => p.postName == model.postName)) == 0)
            {
                Post post = new Post
                {
                    postName = model.postName,
                    descriptionPost = model.descriptionPost
                };
                await db.Posts.AddAsync(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("postName", "Запись с таким именем уже есть");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Post post = await db.Posts.FirstOrDefaultAsync(t => t.postID == id);
                if (post == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                    };
                    return View("Error", error);
                }
                EditPostViewModel model = new EditPostViewModel
                {
                    Id = post.postID,
                    postName = post.postName,
                    descriptionPost = post.descriptionPost
                };
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            int er = 0;
            Post post = await db.Posts.FirstOrDefaultAsync(t => t.postID == model.Id);
            if (ModelState.IsValid && (model.postName == post.postName || (er = db.Posts.Count(p => p.postName == model.postName)) == 0))
            {
                if (post == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                post.postName = model.postName;
                post.descriptionPost = model.descriptionPost;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("postName", "Запись с таким именем уже есть");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(t => t.postID == id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Workers(int? id)
        {
            try
            {
                if (id != null)
                {
                    Post post = await db.Posts.Include(x => x.Workers).ThenInclude(x => x.Post).FirstOrDefaultAsync(t => t.postID == id);
                    if (post == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                    return View(post);
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
            return View();
        }

        public IActionResult CreateWorker(int? postId)
        {
            try
            {
                ViewBag.postID = postId;
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorker(CreateWorkerViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Workers.Count(p => p.fioWorker == model.fioWorker)) == 0)
            {
                try
                {
                    Worker worker = new Worker
                    {
                        fioWorker = model.fioWorker,
                        postID = model.postID,
                        dateOfEmployment = model.dateOfEmployment,
                        dateOfDismissal = model.dateOfDismissal,
                        salary = model.salary
                    };
                    await db.Workers.AddAsync(worker);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Workers", new { id = model.postID });
                }
                catch(Exception ex)
                {

                }
            }
            if (er != 0)
                ModelState.AddModelError("fioWorker", "Запись с таким именем уже есть");
            ViewBag.postID = model.postID;
            return View(model);
        }

        public async Task<IActionResult> EditWorker(int? id)
        {
            if (id != null)
            {
                Worker worker = await db.Workers.Include("Post").FirstOrDefaultAsync(t => t.workerID == id);
                if (worker != null)
                {
                    EditWorkerViewModel model = new EditWorkerViewModel
                    {
                        Id = worker.workerID,
                        fioWorker = worker.fioWorker,
                        postName = worker.Post.postName,
                        dateOfEmployment = worker.dateOfEmployment,
                        dateOfDismissal = worker.dateOfDismissal,
                        salary = worker.salary
                    };
                    return View(model);
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным  id = " + id
                    };
                    return View("Error", error);
                }
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditWorker(EditWorkerViewModel model)
        {
            Worker worker = await db.Workers.FirstOrDefaultAsync(t => t.workerID == model.Id);
            int er = 0;
            if (ModelState.IsValid && (model.fioWorker == worker.fioWorker || (er = db.Workers.Count(p => p.fioWorker == model.fioWorker)) == 0))
            {
                if (worker == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                worker.fioWorker = model.fioWorker;
                worker.dateOfEmployment = model.dateOfEmployment;
                worker.dateOfDismissal = model.dateOfDismissal;
                worker.salary = model.salary;
                await db.SaveChangesAsync();
                return RedirectToAction("Workers", new { id = worker.postID });
            }
            if (er != 0)
                ModelState.AddModelError("fioWorker", "Запись с таким именем уже есть");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWorker(int? id)
        {
            Worker worker = await db.Workers.FirstOrDefaultAsync(t => t.workerID == id);
            int WorkerId = worker.postID;
            db.Workers.Remove(worker);
            await db.SaveChangesAsync();
            return RedirectToAction("Workers", new { id = WorkerId });
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
