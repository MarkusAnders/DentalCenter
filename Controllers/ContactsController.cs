﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalCenter.Models;
using Microsoft.AspNetCore.Authorization;

namespace DentalCenter.Controllers
{
    public class ContactsController : Controller
    {
        private readonly DentalCenterDBContext _context;

        public ContactsController(DentalCenterDBContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin")]
        // GET: Contacts
        public IActionResult Index()
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            var items = _context.Contact.ToList();
            foreach (var item in items)
            {
                contacts.Add(new ContactViewModel
                {
                    ContactId = item.ContactId,
                    ContactSurname= item.ContactSurname,
                    ContactName= item.ContactName,
                    ContactPhone  = item.ContactPhone,
                    ContactMessage = item.ContactMessage,
                });
            }
            return View(contacts);
        }


        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,ContactName,ContactSurname,ContactPhone,ContactMessage")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        [Authorize(Roles = "admin")]
        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,ContactName,ContactSurname,ContactPhone,ContactMessage")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            return View(contact);
        }

        [Authorize(Roles = "admin")]
        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contact == null)
            {
                return Problem("Entity set 'DentalCenterDBContext.Contact'  is null.");
            }
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteContact(List<ContactViewModel> cont) { 
            List<Contact> contacts = new List<Contact>();
            foreach (var item in cont)
            {
                if (item.Emps.Selected)
                {
                    var selectedContact = _context.Contact.Find(item.ContactId);
                    contacts.Add(selectedContact);
                }
            }
            _context.Contact.RemoveRange(contacts);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool ContactExists(int id)
        {
          return (_context.Contact?.Any(e => e.ContactId == id)).GetValueOrDefault();
        }
    }
}