using DreamWedding.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamWedding.Models;
using Microsoft.AspNetCore.Authorization;

namespace DreamWedding.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Posts()
        {
            var posts = _context.Posts.Include(p => p.User).ToList();

            return View(posts);
        }

        public async Task<IActionResult> Approve(int postId)
        {
            var post = _context.Posts.SingleOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Posts");
        }
    }
}
