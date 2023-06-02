using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalCenter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DentalCenter.Areas.Identity.Data;
using System.Numerics;
using Microsoft.IdentityModel.Tokens;

namespace DentalCenter.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly DentalCenterDBContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<DentalCenterUser> _userManager;


        public DoctorsController(DentalCenterDBContext context, IWebHostEnvironment appEnvironment, UserManager<DentalCenterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
              return _context.Doctors != null ? 
                          View(await _context.Doctors.ToListAsync()) :
                          Problem("Entity set 'DentalCenterDBContext.Doctors'  is null.");
        }

        [Authorize(Roles = "admin, doctor")]
        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            if (!doctor.DoctorPhoto.IsNullOrEmpty())
            {
                byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + doctor.DoctorPhoto);

                ViewBag.Photodata = photodata;
            }
            else
            {
                ViewBag.Photodata = null;
            }

            return View(doctor);
        }

        [Authorize(Roles = "admin, doctor")]
        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,Email,DoctorSurname,DoctorName,DoctorPatronymic,DoctorSpecialization,DoctorCabinet,DoctorPhoto")] Doctor doctor, IFormFile? upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/PhotoDoctors/" + upload.FileName;
                    using (var fileStream = new
                    FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    doctor.DoctorPhoto = path;
                }

                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            if (!doctor.DoctorPhoto.IsNullOrEmpty())
            {
                byte[] photodata = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + doctor.DoctorId);
                ViewBag.Photodata = photodata;
            }
            else
            {
                ViewBag.Photodata = null;
            }

            return View(doctor);
        }

        [Authorize(Roles = "admin, doctor")]
        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,Email,DoctorSurname,DoctorName,DoctorPatronymic,DoctorSpecialization,DoctorCabinet,DoctorPhoto")] Doctor doctor, IFormFile? upload)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/PhotoDoctors/" + upload.FileName;
                    using (var fileStream = new
                    FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    if (!doctor.DoctorPhoto.IsNullOrEmpty())
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath +
                       doctor.DoctorPhoto);
                    }
                    doctor.DoctorPhoto = path;
                }

                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
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
            return View(doctor);
        }

        [Authorize(Roles = "admin, doctor")]
        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctors == null)
            {
                return Problem("Entity set 'DentalCenterDBContext.Doctors'  is null.");
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (_context.Doctors?.Any(e => e.DoctorId == id)).GetValueOrDefault();
        }
    }
}
