using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using SecureSeat.Data;  
using SecureSeat.Models;

namespace SecureSeat.Controllers
{
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
        public async Task<IActionResult> Edit(int id, Show @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create(Show @event)
        {
            if (ModelState.IsValid)
            {
                @event.dateCreated = DateTime.Now;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

    }
}
