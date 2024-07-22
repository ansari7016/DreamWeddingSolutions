using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamWedding.Data;
using DreamWedding.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace DreamWeddin.Controllers
{
    public class ReelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReelsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = "User")]

        // GET: Reels
        public async Task<IActionResult> Index()
        {
            var dreamWeddingContext = _context.Reels.Include(r => r.User);
            return View(await dreamWeddingContext.ToListAsync());
        }

        // GET: Reels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reels = await _context.Reels
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReelsId == id);
            if (reels == null)
            {
                return NotFound();
            }

            return View(reels);
        }

        // GET: Reels/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Reels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReelsViewModel reels)
        {
            string Id = HttpContext.Session.GetString("id");

            if (ModelState.IsValid)
            {
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

                string uniqueFileName = null;
                if (reels.VideoFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "reels");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + reels.VideoFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await reels.VideoFile.CopyToAsync(fileStream);
                    }
                }

                var reel = new Reels
                {
                    ReelsVideo = uniqueFileName,
                    Captions = reels.Caption,
                    CreatedAt = DateTime.Now,
                    UserId = Id
                };

                _context.Add(reel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Privacy", "Home");
            }
            return View(reels);
        }

        // GET: Reels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reels = await _context.Reels.FindAsync(id);
            if (reels == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reels.UserId);
            return View(reels);
        }

        // POST: Reels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReelsId,ReelsVideo,Captions,CreatedAt,UserId")] Reels reels)
        {
            if (id != reels.ReelsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReelsExists(reels.ReelsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reels.UserId);
            return View(reels);
        }

        // GET: Reels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reels = await _context.Reels
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReelsId == id);
            if (reels == null)
            {
                return NotFound();
            }

            return View(reels);
        }

        // POST: Reels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reels = await _context.Reels.FindAsync(id);
            if (reels != null)
            {
                _context.Reels.Remove(reels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReelsExists(int id)
        {
            return _context.Reels.Any(e => e.ReelsId == id);
        }
    }
}
