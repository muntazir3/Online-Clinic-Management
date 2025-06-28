using EPROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EPROJECT.Controllers
{
    public class insuranceController : Controller
    {
        private readonly insurance_companyContext _context;

        public insuranceController(insurance_companyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           

return View();
        }

        public IActionResult error()
        {
            return View();
        }
        public IActionResult about()
        {
            return View();
        }

        public IActionResult doctor()
        {
            return View();
        }
        public IActionResult treatment()
        {
            return View();
        }
        public IActionResult appoinment()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult feature()
        {
            return View();
        }
        public IActionResult service()
        {
            return View();
        }
        public IActionResult team()
        {
            return View();
        }
        [Authorize]

        [Authorize(Policy = "NonAdminOnly")]
        public IActionResult review()
        {

            var policy = _context.Policies.Include(e => e.Company).ToList();
            return View(policy);
            
        }


        public IActionResult Btn_Description(int companyId)
        {
            // Fetch the company details, including the related policies
            var company = _context.CompanyDetails
                                   .FirstOrDefault(c => c.Companyid == companyId);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            // Fetch the policy associated with the company using the foreign key
            var policy = _context.Policies
                                 .FirstOrDefault(p => p.Companyid == companyId);

            if (policy == null)
            {
                return NotFound("Policy not found for this company");
            }

            // Pass the Policy ID to the view
            ViewData["PolicyID"] = policy.Policyid;

            // Pass the company details to the view
            return View(company);
        }




        [HttpPost]
        public async Task<IActionResult> ApplyForPolicy(int companyId, int policyId)
        {
            // Check if companyId and policyId are valid
            if (companyId <= 0 || policyId <= 0)
            {
                return NotFound("Invalid company or policy.");
            }

            var policy = await _context.Policies.FirstOrDefaultAsync(p => p.Policyid == policyId);
            var company = await _context.CompanyDetails.FirstOrDefaultAsync(c => c.Companyid == companyId);

            if (policy == null || company == null)
            {
                return NotFound("Policy or Company not found.");
            }

            // Get the logged-in employee's ID from the claims
            var empIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Using Claims to get the employee ID
            if (string.IsNullOrEmpty(empIdString))
            {
                return Unauthorized("You must be logged in to apply for a policy.");
            }

            int empId;
            if (!int.TryParse(empIdString, out empId))
            {
                return Unauthorized("Invalid employee ID.");
            }

            // Check if the employee exists in the EmpRegister table
            var emp = await _context.EmpRegisters.FirstOrDefaultAsync(e => e.Empid == empId);
            if (emp == null)
            {
                return Unauthorized("Employee not found.");
            }

            bool alreadyApplied = await _context.PolicyRequestDetails.AnyAsync(pr =>
                pr.Empid == empId &&
                pr.Policyid == policyId &&
                pr.Companyid == companyId);

            if (alreadyApplied)
            {
                TempData["ErrorMessage"] = "You have already applied for this policy.";
                return RedirectToAction("Index", "insurance"); // Adjust redirection as needed
            }

            // Check if the employee applied for any policy within the last 30 days
            DateTime thirtyDaysAgo = DateTime.Now.AddDays(-30);
            bool appliedWithin30Days = await _context.PolicyRequestDetails.AnyAsync(pr =>
                pr.Empid == empId &&
                pr.Requestdate >= thirtyDaysAgo);

            if (appliedWithin30Days)
            {
                TempData["ErrorMessages"] = "You cannot apply for another policy within 30 days.";
                return RedirectToAction("Index", "insurance"); // Adjust redirection as needed
            }

            // Create a new PolicyRequestDetail object
            var policyRequest = new PolicyRequestDetail
            {
                Requestdate = DateTime.Now,
                Empid = empId, // Valid employee ID from Claims
                Policyid = policy.Policyid,
                Policyname = policy.Policyname,
                Policyamount = policy.Amount,
                Emi = policy.Emi,
                Companyid = company.Companyid,
                Companyname = company.CompanyName,
                Status = "Pending"
            };

            // Add the request to the database
            _context.PolicyRequestDetails.Add(policyRequest);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Policy request submitted successfully.";
            return RedirectToAction("Index", "insurance"); // Adjust redirection as needed
        }




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewPolicyRequests()
        {
            // Fetch all policy requests, including related employee and policy details
            var requests = await _context.PolicyRequestDetails
                .Include(r => r.Emp)        // Fetch employee details
                .Include(r => r.Policy)     // Fetch policy details
             
                .ToListAsync();

            return View(requests);
        }


        [HttpPost]
        public IActionResult ApproveRequest(int requestId)
        {
            var request = _context.PolicyRequestDetails.Find(requestId);
            if (request != null)
            {
                request.Status = "Approved"; // Change status to Approved
                _context.SaveChanges();

                TempData["Message"] = "Request approved successfully.";
            }
            else
            {
                TempData["Message"] = "Request not found.";
            }

            return RedirectToAction("ViewPolicyRequests"); // Redirect back to the view with the updated status
        }

        // Reject Policy Request (Form POST)
        [HttpPost]
        public IActionResult RejectRequest(int requestId)
        {
            var request = _context.PolicyRequestDetails.Find(requestId);
            if (request != null)
            {
                request.Status = "Rejected"; // Change status to Rejected
                _context.SaveChanges();

                TempData["RMessage"] = "Request rejected successfully.";
            }
            else
            {
                TempData["RMessage"] = "Request not found.";
            }

            return RedirectToAction("ViewPolicyRequests"); // Redirect back to the view with the updated status
        }

       
    }

}

