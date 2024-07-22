using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using DreamWedding.Models;
using DreamWedding.Data;

public class PostController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor ca;

    public PostController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor acs)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        ca = acs;
    }
    [Authorize(Roles = "User")]

    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(PostViewModel model)
    {
        if (ModelState.IsValid)
        {
            string Id = HttpContext.Session.GetString("id");
            /*var claimsIdentity = User.Identity as ClaimsIdentity;
            var googleId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            var dbUser = _context.Users.SingleOrDefault(u => u.GoogleId == googleId);

            if (dbUser == null)
            {
                dbUser = new User
                {
                    GoogleId = googleId,
                    Name = userName
                };
                _context.Users.Add(dbUser);
                await _context.SaveChangesAsync();
            }*/

            // Save image to server
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }
            }

            var post = new Post
            {
                ImagePath = uniqueFileName,
                Caption = model.Caption,
                CreatedAt = DateTime.Now,
                IsApproved = false,
                UserId = Id
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }
}
