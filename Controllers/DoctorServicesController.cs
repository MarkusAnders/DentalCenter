using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalCenter.Models;

namespace DentalCenter.Controllers
{
    public class DoctorServicesController : Controller
    {
        private readonly DentalCenterDBContext _context;

        public DoctorServicesController(DentalCenterDBContext context)
        {
            _context = context;
        }

        // GET: DoctorServices
        public async Task<IActionResult> Index()
        {
            var dentalCenterDBContext = _context.DoctorService.Include(d => d.Doctor).Include(d => d.Service);
            return View(await dentalCenterDBContext.ToListAsync());
        }

        // GET: DoctorServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DoctorService == null)
            {
                return NotFound();
            }

            var doctorService = await _context.DoctorService
                .Include(d => d.Doctor)
                .Include(d => d.Service)
                .FirstOrDefaultAsync(m => m.DoctorServiceId == id);
            if (doctorService == null)
            {
                return NotFound();
            }

            return View(doctorService);
        }

        // GET: DoctorServices/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId");
            return View();
        }

        // POST: DoctorServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorServiceId,DoctorId,ServiceId")] DoctorService doctorService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", doctorService.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", doctorService.ServiceId);
            return View(doctorService);
        }

        // GET: DoctorServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DoctorService == null)
            {
                return NotFound();
            }

            var doctorService = await _context.DoctorService.FindAsync(id);
            if (doctorService == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", doctorService.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", doctorService.ServiceId);
            return View(doctorService);
        }

        // POST: DoctorServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorServiceId,DoctorId,ServiceId")] DoctorService doctorService)
        {
            if (id != doctorService.DoctorServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorServiceExists(doctorService.DoctorServiceId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", doctorService.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", doctorService.ServiceId);
            return View(doctorService);
        }

        // GET: DoctorServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DoctorService == null)
            {
                return NotFound();
            }

            var doctorService = await _context.DoctorService
                .Include(d => d.Doctor)
                .Include(d => d.Service)
                .FirstOrDefaultAsync(m => m.DoctorServiceId == id);
            if (doctorService == null)
            {
                return NotFound();
            }

            return View(doctorService);
        }

        // POST: DoctorServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoctorService == null)
            {
                return Problem("Entity set 'DentalCenterDBContext.DoctorService'  is null.");
            }
            var doctorService = await _context.DoctorService.FindAsync(id);
            if (doctorService != null)
            {
                _context.DoctorService.Remove(doctorService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorServiceExists(int id)
        {
          return (_context.DoctorService?.Any(e => e.DoctorServiceId == id)).GetValueOrDefault();
        }
    }
}
