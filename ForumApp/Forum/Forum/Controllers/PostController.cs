using Forum.Services.DTO;
using Forum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAll();

            return View(posts);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            await _postService.Create(post);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string id)
        {
            var post = await _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            await _postService.Update(id, post);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this._postService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
