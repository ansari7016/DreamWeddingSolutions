using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamWedding.Data;
using DreamWedding.Models;

namespace DreamWedding.Controllers
{
    public class AdvertismentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdvertismentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Advertisments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertisments.ToListAsync());
        }

        // GET: Advertisments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisment = await _context.Advertisments
                .FirstOrDefaultAsync(m => m.AdvertismentId == id);
            if (advertisment == null)
            {
                return NotFound();
            }

            return View(advertisment);
        }

        // GET: Advertisments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advertisments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advertisment advertisment, IFormFile image)
        {
            if (image != null)
            {
                string ext = Path.GetExtension(image.FileName);
                if (ext == ".jpg" || ext == ".png")
                {
                    string d = Path.Combine(_webHostEnvironment.WebRootPath, "images/ads");
                    var fname = Path.GetFileName(image.FileName);
                    string filepath = Path.Combine(d, fname);
                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(fs);
                    }
                    advertisment.AdsImage = @"images/ads/" + fname;
                    _context.Add(advertisment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }
            return View(advertisment);
        }

        // GET: Advertisments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisment = await _context.Advertisments.FindAsync(id);
            if (advertisment == null)
            {
                return NotFound();
            }
            return View(advertisment);
        }

        // POST: Advertisments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertismentId,AdsImage,AdsLink")] Advertisment advertisment)
        {
            if (id != advertisment.AdvertismentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertismentExists(advertisment.AdvertismentId))
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
            return View(advertisment);
        }

        // GET: Advertisments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisment = await _context.Advertisments
                .FirstOrDefaultAsync(m => m.AdvertismentId == id);
            if (advertisment == null)
            {
                return NotFound();
            }

            return View(advertisment);
        }

        // POST: Advertisments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisment = await _context.Advertisments.FindAsync(id);
            if (advertisment != null)
            {
                _context.Advertisments.Remove(advertisment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertismentExists(int id)
        {
            return _context.Advertisments.Any(e => e.AdvertismentId == id);
        }
    }
}
