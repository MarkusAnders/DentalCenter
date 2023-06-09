﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalCenter.Models;

namespace DentalCenter.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DentalCenterDBContext _context;

        public AppointmentsController(DentalCenterDBContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var dentalCenterDBContext = _context.Appointment.Include(a => a.Client).Include(a => a.Doctor).Include(a => a.Service);
            return View(await dentalCenterDBContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Client)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Clients, "Id", "ClientName");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Email");
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,Id,ServiceId,DoctorId,AppointmentDateVisiting")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Clients, "Id", "ClientName", appointment.Id);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Email", appointment.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Clients, "Id", "ClientName", appointment.Id);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Email", appointment.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,Id,ServiceId,DoctorId,AppointmentDateVisiting")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["Id"] = new SelectList(_context.Clients, "Id", "ClientName", appointment.Id);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Email", appointment.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Client)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'DentalCenterDBContext.Appointment'  is null.");
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
