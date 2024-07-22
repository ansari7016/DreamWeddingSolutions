using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamWedding.Data;
using DreamWedding.Models;
using Microsoft.AspNetCore.Authorization;

namespace DreamWedding.Controllers
{
    public class PostsCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "User")]

        // GET: PostsComments
        public async Task<IActionResult> Index()
        {
            var dreamWeddingContext = _context.PostsComments.Include(p => p.Post).Include(p => p.User);
            return View(await dreamWeddingContext.ToListAsync());
        }

        // GET: PostsComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postsComments = await _context.PostsComments
                .Include(p => p.Post)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostsCommentsId == id);
            if (postsComments == null)
            {
                return NotFound();
            }

            return View(postsComments);
        }

        // GET: PostsComments/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PostsComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostsCommentsId,Comments,PostId,UserId")] PostsComments postsComments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postsComments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postsComments.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", postsComments.UserId);
            return View(postsComments);
        }

        // GET: PostsComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postsComments = await _context.PostsComments.FindAsync(id);
            if (postsComments == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postsComments.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", postsComments.UserId);
            return View(postsComments);
        }

        // POST: PostsComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostsCommentsId,Comments,PostId,UserId")] PostsComments postsComments)
        {
            if (id != postsComments.PostsCommentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postsComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsCommentsExists(postsComments.PostsCommentsId))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postsComments.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", postsComments.UserId);
            return View(postsComments);
        }

        // GET: PostsComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postsComments = await _context.PostsComments
                .Include(p => p.Post)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostsCommentsId == id);
            if (postsComments == null)
            {
                return NotFound();
            }

            return View(postsComments);
        }

        // POST: PostsComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postsComments = await _context.PostsComments.FindAsync(id);
            if (postsComments != null)
            {
                _context.PostsComments.Remove(postsComments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsCommentsExists(int id)
        {
            return _context.PostsComments.Any(e => e.PostsCommentsId == id);
        }
    }
}
