using DreamWedding.Data;
using DreamWedding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using DreamWedding.Data;
//using DreamWedding.Data;
//using DreamWedding.Models;
using System.Diagnostics;

namespace DreamWedding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor ca;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> usermanager, IHttpContextAccessor acs, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = usermanager;
            ca = acs;
            _context = context;
        }
        [Authorize(Roles = "User")]

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            //For Fetching User Id
            var user = await _userManager.GetUserAsync(User); // Get the current user.
            if (user != null)
            {
                var userId = user.Id;
                var Email = user.Email;
                var Name = user.Name;
                ca.HttpContext.Session.SetString("id", userId);
                ca.HttpContext.Session.SetString("u", Email);
                ca.HttpContext.Session.SetString("f", Name);
                // You can store other user-related data in the session as well if needed.
            }
            var viewModel = new ShowViewModel
            {
                Users = _context.Users.ToList(),
                Advertisements = _context.Advertisments.ToList(),
                Posts = _context.Posts
                        .Include(p => p.User)
                        .Where(p => p.IsApproved)
                        .ToList(),

            };
            //var posts = _context.Posts.Include(p => p.User).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SavePost(int postId)
        {
            string Id = HttpContext.Session.GetString("id");

            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            var savedPost = new SavePosts
            {
                UserId = Id,
                PostId = postId
            };

            _context.SavePosts.Add(savedPost);
            await _context.SaveChangesAsync();

            return Ok("Post saved successfully.");
        }

        [HttpPost]
        public IActionResult AddComment(int postId, int userId, string commentText)
        {
            string Id = HttpContext.Session.GetString("id");
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            //var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            
                var comment = new PostsComments
                {
                    PostId = postId,
                    UserId = Id,
                    Comments = commentText
                };

                _context.PostsComments.Add(comment);
                _context.SaveChanges();

             

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(int postId, int userId)
        {
            string Id = HttpContext.Session.GetString("id");
            var post = await _context.Posts.Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return Json(new { success = false });
            }

            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            

            var like = new PostsLike
            {
                PostId = postId,
                UserId = Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.PostsLikes.Add(like);
            await _context.SaveChangesAsync();

            var likesCount = post.Likes.Count;

            return Json(new { success = true, likesCount });
        }


        public IActionResult Privacy()
        {
            var viewModel = new ShowViewModel
            {
                Users = _context.Users.ToList(),
                Advertisements = _context.Advertisments.ToList(),
                Reels = _context.Reels
                       .Include(r => r.User)
                       .ToList(),

            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddReelsComment(int reelsId, int userId, string commentText)
        {
            string Id = HttpContext.Session.GetString("id");
            var reels = _context.Reels.FirstOrDefault(p => p.ReelsId == reelsId);
            //var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            
                var comment = new ReelsComments
                {
                    ReelsId = reelsId,
                    UserId = Id,
                    Comments = commentText
                };

                _context.ReelsComments.Add(comment);
                _context.SaveChanges();

            

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> ReelsLikePost(int reelId, int userId)
        {
            string Id = HttpContext.Session.GetString("id");
            var reel = await _context.Reels.Include(p => p.ReelsLikes).FirstOrDefaultAsync(p => p.ReelsId == reelId);
            if (reel == null)
            {
                return Json(new { success = false });
            }

            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            

            var like = new ReelsLikes
            {
                ReelsId = reelId,
                UserId = Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.ReelsLikes.Add(like);
            await _context.SaveChangesAsync();

            var likesCount = reel.ReelsLikes.Count;

            return Json(new { success = true, likesCount });
        }

        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> ChatPage(int? userId)
        {
            //if (userId == null)
            //{
            //    return NotFound();
            //}

            //var uperwaleID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var user = await _userManager.GetUserAsync(User);
            //var currentUserId = user?.Id;

            //var messages = await _context.Chats
            //            .Include(c => c.Sender)
            //            .Include(c => c.Receiver)
            //            .Where(c => (c.SenderId == currentUserId && c.ReceiverId == userId) ||
            //                         (c.SenderId == userId && c.ReceiverId == currentUserId))
            //            .OrderBy(c => c.CreatedAt)
            //            .ToListAsync();

            //if (messages == null)
            //{
            //    messages = new List<Chats>();
            //}

            //ViewBag.Users = await _context.Users.ToListAsync();
            //ViewData["SelectedUserId"] = userId;

            return View();
        }

        /*public async Task<IActionResult> Chat(int? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var messages = await _context.Chats
                        .Include(c => c.Sender)
                        .Include(c => c.Receiver)
                        .Where(c => (c.SenderId == currentUserId && c.ReceiverId == userId) ||
                                     (c.SenderId == userId && c.ReceiverId == currentUserId))
                        .OrderBy(c => c.CreatedAt)
                        .ToListAsync();

            if (messages == null)
            {
                messages = new List<Chats>();
            }

            ViewBag.Users = await _context.Users.ToListAsync();
            ViewData["SelectedUserId"] = userId;

            return View(messages);
        }*/

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send([Bind("SenderId,ReceiverId,Content")] Chats message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Chat), new { userId = message.ReceiverId });
            }
            return View("Chat", new { userId = message.ReceiverId });
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}