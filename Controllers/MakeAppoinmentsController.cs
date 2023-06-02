using DentalCenter.Areas.Identity.Data;
using DentalCenter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace DentalCenter.Controllers
{
    [Authorize]
    public class MakeAppoinmentsController : Controller
    {
        private readonly DentalCenterDBContext _context;
        private readonly UserManager<DentalCenterUser> _userManager;

        public MakeAppoinmentsController(DentalCenterDBContext context, UserManager<DentalCenterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> SelectService()
        {
            return View(await _context.Service.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SelectService(int serviceId)
        {
            Service service = await _context.Service.FindAsync(serviceId);
            if (service == null)
            {
                return NotFound();
            }
            ViewBag.ServiceId = serviceId;
            return View(nameof(SelectDoctor), await _context.Doctors.ToListAsync());
        }

        public async Task<IActionResult> SelectDoctor()
        {
            return View(await _context.Doctors.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SelectDoctor(int serviceId, int doctorId)
        {
            Doctor doctor = await _context.Doctors.FindAsync(doctorId);
            Service service = await _context.Service.FindAsync(serviceId);
            if (doctor == null || service == null)
            {
                return NotFound();
            }
            ViewBag.ServiceId = serviceId;
            ViewBag.DoctorId = doctorId;
            return View(nameof(ChooseDate), await _context.Appointment.ToListAsync());
        }

        public async Task<IActionResult> ChooseDate()
        {
            return View(await _context.Appointment.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> ChooseDate(int serviceId, int doctorId, DateTime appointmentDateVisiting)
        {
            if (User.IsInRole("client"))
            {
                Client currentClient = await _context.Clients.FirstOrDefaultAsync(e => e.DentalCenterUserId == _userManager.GetUserId(HttpContext.User));

                ViewBag.Id = currentClient.Id;
            }

            var appointments = await _context.Appointment.ToListAsync();
            if (appointments == null)
            {
                return NotFound();
            }
            Doctor doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            Service service = _context.Service.FirstOrDefault(d => d.ServiceId == serviceId);
            ViewBag.Doctor = doctor;
            ViewBag.Service = service;
            ViewBag.AppointmentDateVisiting = appointmentDateVisiting.ToShortDateString();
            if (appointments.Count(v => v.ServiceId == serviceId && v.DoctorId == doctorId && v.AppointmentDateVisiting == appointmentDateVisiting) > 0) { return View(appointments); }

            return View(nameof(ConfirmRecord));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRecord([Bind("ServiceId,DoctorId,AppointmentDateVisiting")] Appointment appointments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointments);
                await _context.SaveChangesAsync();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FirstName", appointments.DoctorId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "FirstName", appointments.ServiceId);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
