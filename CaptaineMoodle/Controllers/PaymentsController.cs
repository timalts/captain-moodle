using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Models;
using Microsoft.AspNetCore.Authorization;
using CaptaineMoodle.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Specialized;

namespace CaptaineMoodle.Controllers
{
    [AllowAnonymous]
    public class PaymentsController : Controller
    {

        private readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;

        //Method to get the current login user
        public PaymentsController(AuthDbContext context, UserManager<User> userManager) 
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            //var payments = await _context.Payment.FindAsync(usr.Id);
            return View(await _context.Payment.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, User, Amount,Semester,Paid")] Payment payment, ClaimsPrincipal principal)
        {
            var users = await _userManager.Users.ToListAsync();
            SelectList list = new SelectList(users);
            ViewBag.Users = list;
            if (User.IsInRole("Admin") && ModelState.IsValid)
            {
                var nvc = Request.Form;
                string user_post = nvc["User"];
                //system.diagnostics.debug.writeline(user_post);
                var sameuser = _userManager.Users.FirstOrDefault(u => u.Email == user_post);
                //system.diagnostics.debug.writeline("user is: " + sameuser.email);
                payment.User = sameuser;
            }
            else if (User.IsInRole("Student") && ModelState.IsValid)
            {
                var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
                payment.User = current_User;
            }
            _context.Add(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User, Amount,Semester,Paid")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
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
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}