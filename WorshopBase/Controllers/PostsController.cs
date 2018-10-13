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
    public class PostsController : Controller
    {
        WorkshopContext db;

        public PostsController(WorkshopContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            var postsList = db.Posts.ToList();
            List<PostViewModel> list = new List<PostViewModel>();
            foreach (var item in postsList)
            {
                list.Add(new PostViewModel
                {
                    Id = item.postID,
                    postName = item.postName,
                    descriptionPost = item.descriptionPost
                });
            }
            return View(list);
        }
    }
}
