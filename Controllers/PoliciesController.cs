using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EPROJECT.Models;
using Microsoft.AspNetCore.Authorization;

namespace EPROJECT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PoliciesController : Controller
    {
        private readonly insurance_companyContext _context;

        public PoliciesController(insurance_companyContext context)
        {
            _context = context;
        }

        // GET: Policies
        public async Task<IActionResult> Index()
        {
            var insurance_companyContext = _context.Policies.Include(p => p.Company);
            return View(await insurance_companyContext.ToListAsync());
        }

        // GET: Policies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Policyid == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // GET: Policies/Create
        public IActionResult Create()
        {
            ViewData["CompanyName"] = new SelectList(_context.CompanyDetails, "Companyid", "CompanyName");
            return View();
        }

        // POST: Policies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Policyid,Policyname,Policydesc,Amount,Emi,Companyid,Medicalid")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyName"] = new SelectList(_context.CompanyDetails, "Companyid", "ComCompanyNamepanyid", policy.Companyid);
            return View(policy);
        }

        // GET: Policies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            ViewData["CompanyName"] = new SelectList(_context.CompanyDetails, "Companyid", "CompanyName", policy.Companyid);
            return View(policy);
        }

        // POST: Policies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Policyid,Policyname,Policydesc,Amount,Emi,Companyid,Medicalid")] Policy policy)
        {
            if (id != policy.Policyid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.Policyid))
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
            ViewData["CompanyName"] = new SelectList(_context.CompanyDetails, "Companyid", "CompanyName", policy.Companyid);
            return View(policy);
        }

        // GET: Policies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Policyid == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // POST: Policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the policy and its related records
            var policy = await _context.Policies
                 
                .Include(p => p.PolicyRequestDetails)   // Include request details
                .FirstOrDefaultAsync(p => p.Policyid == id);

            if (policy != null)
            {
               

                // Remove all related policy requests
                _context.PolicyRequestDetails.RemoveRange(policy.PolicyRequestDetails);

                // Remove the policy
                _context.Policies.Remove(policy);

                // Save changes
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool PolicyExists(int id)
        {
          return (_context.Policies?.Any(e => e.Policyid == id)).GetValueOrDefault();
        }
    }
}
