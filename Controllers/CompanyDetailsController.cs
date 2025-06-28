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
    public class CompanyDetailsController : Controller
    {
        private readonly insurance_companyContext _context;

        public CompanyDetailsController(insurance_companyContext context)
        {
            _context = context;
        }

        // GET: CompanyDetails
        public async Task<IActionResult> Index()
        {
              return _context.CompanyDetails != null ? 
                          View(await _context.CompanyDetails.ToListAsync()) :
                          Problem("Entity set 'insurance_companyContext.CompanyDetails'  is null.");
        }

        // GET: CompanyDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompanyDetails == null)
            {
                return NotFound();
            }

            var companyDetail = await _context.CompanyDetails
                .FirstOrDefaultAsync(m => m.Companyid == id);
            if (companyDetail == null)
            {
                return NotFound();
            }

            return View(companyDetail);
        }

        // GET: CompanyDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Companyid,CompanyName,Address,Phone,CompanyUrl")] CompanyDetail companyDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyDetail);
        }

        // GET: CompanyDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompanyDetails == null)
            {
                return NotFound();
            }

            var companyDetail = await _context.CompanyDetails.FindAsync(id);
            if (companyDetail == null)
            {
                return NotFound();
            }
            return View(companyDetail);
        }

        // POST: CompanyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Companyid,CompanyName,Address,Phone,CompanyUrl")] CompanyDetail companyDetail)
        {
            if (id != companyDetail.Companyid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDetailExists(companyDetail.Companyid))
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
            return View(companyDetail);
        }

        // GET: CompanyDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompanyDetails == null)
            {
                return NotFound();
            }

            var companyDetail = await _context.CompanyDetails
                .FirstOrDefaultAsync(m => m.Companyid == id);
            if (companyDetail == null)
            {
                return NotFound();
            }

            return View(companyDetail);
        }

        // POST: CompanyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the company and its related policies
            var company = await _context.CompanyDetails
                .Include(c => c.Policies) // Include related policies
               
                .Include(c => c.Policies)
                .ThenInclude(p => p.PolicyRequestDetails)  // Include requests for policies
                .FirstOrDefaultAsync(c => c.Companyid == id);

            if (company != null)
            {

                TempData["CDelete"] = "Company deleted successfully.";
                // For each policy, remove its dependent records
                foreach (var policy in company.Policies)
                {
                    
                    _context.PolicyRequestDetails.RemoveRange(policy.PolicyRequestDetails);
                }

                // Remove the company's policies
                _context.Policies.RemoveRange(company.Policies);

                // Remove the company
                _context.CompanyDetails.Remove(company);

                // Save changes
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool CompanyDetailExists(int id)
        {
          return (_context.CompanyDetails?.Any(e => e.Companyid == id)).GetValueOrDefault();
        }
    }
}
