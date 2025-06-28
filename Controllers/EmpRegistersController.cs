using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EPROJECT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EPROJECT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmpRegistersController : Controller
    {
        private readonly insurance_companyContext _context;

        public EmpRegistersController(insurance_companyContext context)
        {
            _context = context;
        }

        // GET: EmpRegisters
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["searchTerm"] = searchTerm;  // Set the searchTerm in ViewData

            var employees = _context.EmpRegisters.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Perform a case-insensitive search on multiple fields (Username, Email, City)
                employees = employees.Where(e => e.Username.Contains(searchTerm) ||
                                                 e.Email.Contains(searchTerm) ||
                                                 e.City.Contains(searchTerm))
                                     .OrderBy(e => e.Username.Contains(searchTerm) ? 0 : 1); // Priority to matching username first
            }

            // Return the filtered or full list
            return View(await employees.ToListAsync());
        }




        // GET: EmpRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmpRegisters == null)
            {
                return NotFound();
            }

            var empRegister = await _context.EmpRegisters
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (empRegister == null)
            {
                return NotFound();
            }

            return View(empRegister);
        }

        // GET: EmpRegisters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpRegisters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Empid,Username,Email,Password,Designation,JoinDate,Salary,Address,Contactno,City")] EmpRegister empRegister)
        {
            if (ModelState.IsValid)
            {
                TempData["emp"] = "Employee added successfully.";

                _context.Add(empRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empRegister);
        }

        private string? HashPassword(string? password)
        {
            throw new NotImplementedException();
        }

        // GET: EmpRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmpRegisters == null)
            {
                return NotFound();
            }

            var empRegister = await _context.EmpRegisters.FindAsync(id);
            if (empRegister == null)
            {
                return NotFound();
            }
            return View(empRegister);
        }

        // POST: EmpRegisters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Empid,Username,Email,Password,Designation,JoinDate,Salary,Address,Contactno,City")] EmpRegister empRegister)
        {
            if (id != empRegister.Empid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                

                try
                {
                    _context.Update(empRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpRegisterExists(empRegister.Empid))
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
            return View(empRegister);
        }

        // GET: EmpRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmpRegisters == null)
            {
                return NotFound();
            }

            var empRegister = await _context.EmpRegisters
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (empRegister == null)
            {
                return NotFound();
            }

            return View(empRegister);
        }

        // POST: EmpRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the employee and their related records
            var empRegister = await _context.EmpRegisters
                .Include(e => e.PolicyRequestDetails)  // Include related requests
                .FirstOrDefaultAsync(e => e.Empid == id);

            if (empRegister != null)
            {

                TempData["EDelete"] = "Employee deleted successfully.";
                // Remove all related policy requests
                _context.PolicyRequestDetails.RemoveRange(empRegister.PolicyRequestDetails);

                // Remove the employee
                _context.EmpRegisters.Remove(empRegister);

                // Save changes
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool EmpRegisterExists(int id)
        {
          return (_context.EmpRegisters?.Any(e => e.Empid == id)).GetValueOrDefault();
        }
    }
}
