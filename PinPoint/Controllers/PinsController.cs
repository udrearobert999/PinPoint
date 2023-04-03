using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinPoint.Entities;

namespace PinPoint.Controllers
{
    public class PinsController : Controller
    {
        private readonly PinPointContext _context;

        public PinsController(PinPointContext context)
        {
            _context = context;
        }

        // GET: Pins
        public async Task<IActionResult> Index()
        {
              return _context.Pins != null ? 
                          View(await _context.Pins.ToListAsync()) :
                          Problem("Entity set 'PinPointContext.Pins'  is null.");
        }

        // GET: Pins/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Pins == null)
            {
                return NotFound();
            }

            var pin = await _context.Pins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pin == null)
            {
                return NotFound();
            }

            return View(pin);
        }

        // GET: Pins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Picture,UserId")] Pin pin)
        {
            if (ModelState.IsValid)
            {
                pin.Id = Guid.NewGuid();
                _context.Add(pin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pin);
        }

        // GET: Pins/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Pins == null)
            {
                return NotFound();
            }

            var pin = await _context.Pins.FindAsync(id);
            if (pin == null)
            {
                return NotFound();
            }
            return View(pin);
        }

        // POST: Pins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description,Picture,UserId")] Pin pin)
        {
            if (id != pin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PinExists(pin.Id))
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
            return View(pin);
        }

        // GET: Pins/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Pins == null)
            {
                return NotFound();
            }

            var pin = await _context.Pins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pin == null)
            {
                return NotFound();
            }

            return View(pin);
        }

        // POST: Pins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Pins == null)
            {
                return Problem("Entity set 'PinPointContext.Pins'  is null.");
            }
            var pin = await _context.Pins.FindAsync(id);
            if (pin != null)
            {
                _context.Pins.Remove(pin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PinExists(Guid id)
        {
          return (_context.Pins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
