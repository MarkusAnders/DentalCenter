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
    public class DoctorSpecializationsController : Controller
    {
        private readonly DentalCenterDBContext _context;

        public DoctorSpecializationsController(DentalCenterDBContext context)
        {
            _context = context;
        }

        // GET: DoctorSpecializations
        public async Task<IActionResult> Index()
        {
            var dentalCenterDBContext = _context.DoctorSpecialization.Include(d => d.Doctor).Include(d => d.Specialization);
            return View(await dentalCenterDBContext.ToListAsync());
        }

        // GET: DoctorSpecializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DoctorSpecialization == null)
            {
                return NotFound();
            }

            var doctorSpecialization = await _context.DoctorSpecialization
                .Include(d => d.Doctor)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.DoctorSpecializationId == id);
            if (doctorSpecialization == null)
            {
                return NotFound();
            }

            return View(doctorSpecialization);
        }

        // GET: DoctorSpecializations/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationId");
            return View();
        }

        // POST: DoctorSpecializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorSpecializationId,DoctorId,SpecializationId")] DoctorSpecialization doctorSpecialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorSpecialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", doctorSpecialization.DoctorId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationId", doctorSpecialization.SpecializationId);
            return View(doctorSpecialization);
        }

        // GET: DoctorSpecializations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DoctorSpecialization == null)
            {
                return NotFound();
            }

            var doctorSpecialization = await _context.DoctorSpecialization.FindAsync(id);
            if (doctorSpecialization == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", doctorSpecialization.DoctorId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationId", doctorSpecialization.SpecializationId);
            return View(doctorSpecialization);
        }

        // POST: DoctorSpecializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorSpecializationId,DoctorId,SpecializationId")] DoctorSpecialization doctorSpecialization)
        {
            if (id != doctorSpecialization.DoctorSpecializationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorSpecialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorSpecializationExists(doctorSpecialization.DoctorSpecializationId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", doctorSpecialization.DoctorId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationId", doctorSpecialization.SpecializationId);
            return View(doctorSpecialization);
        }

        // GET: DoctorSpecializations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DoctorSpecialization == null)
            {
                return NotFound();
            }

            var doctorSpecialization = await _context.DoctorSpecialization
                .Include(d => d.Doctor)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.DoctorSpecializationId == id);
            if (doctorSpecialization == null)
            {
                return NotFound();
            }

            return View(doctorSpecialization);
        }

        // POST: DoctorSpecializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoctorSpecialization == null)
            {
                return Problem("Entity set 'DentalCenterDBContext.DoctorSpecialization'  is null.");
            }
            var doctorSpecialization = await _context.DoctorSpecialization.FindAsync(id);
            if (doctorSpecialization != null)
            {
                _context.DoctorSpecialization.Remove(doctorSpecialization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorSpecializationExists(int id)
        {
          return (_context.DoctorSpecialization?.Any(e => e.DoctorSpecializationId == id)).GetValueOrDefault();
        }
    }
}
