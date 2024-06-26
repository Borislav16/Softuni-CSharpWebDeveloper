﻿using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumAppDbContext data;

        public PostController(ForumAppDbContext _data)
        {
            data = _data;   
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var posts = await data
                .Posts
                .Select(p => new PostViewModel()
                { 
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
                .ToListAsync();

            return View(posts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostFormModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content
            };

            data.Add(post);
            data.SaveChanges();
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await data.Posts.FindAsync(id);

            return View(new PostFormModel()
            {
                Title = post.Title,
                Content = post.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit
            (int id, PostFormModel model)
        {
            var post = await data.Posts.FindAsync(id);

            post.Title = model.Title;
            post.Content = model.Content;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await data.Posts.FindAsync(id);

            data.Posts.Remove(post);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
