using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using SecureSeat.Data;  
using SecureSeat.Models;
using Microsoft.AspNetCore.Authorization;

namespace SecureSeat.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;
        
        public EventsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _context.Shows
                .OrderBy(e => e.Date)
                .ToListAsync();
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }

        //get for edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shows == null)
            {
                return NotFound();
            }
            var @event = await _context.Shows.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        //post for edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Show @event, IFormFile imageFile)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        @event.ImageUrl = $"/Images/{fileName}";
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(@event);
        }

        private bool ShowExists(int id)
        {
            throw new NotImplementedException();
        }

        //get for delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shows == null)
            {
                return NotFound();
            }
            var @event = await _context.Shows.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        //post for delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shows == null)
            {
                return Problem("Entity set 'AppDbContext.Shows'  is null.");
            }
            var @event = await _context.Shows.FindAsync(id);
            if (@event != null)
            {
                _context.Shows.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //post for create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Show @event, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
               if (imageFile != null && imageFile.Length > 0)
               {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var imageUrl = $"/Images/{fileName}";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    @event.ImageUrl = imageUrl;
                }
                else
                {
                    //default image url
                    @event.ImageUrl = "https://placehold.co/400x275";
                }

                @event.dateCreated = DateTime.Now;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

    }
}
