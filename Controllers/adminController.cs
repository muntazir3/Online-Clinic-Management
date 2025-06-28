using EPROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EPROJECT.Controllers
{
 
    [Authorize(Roles ="Admin")]
    public class adminController : Controller
    {

        private readonly insurance_companyContext _context;

        public adminController(insurance_companyContext context)
        {
            _context = context;
        }

        public IActionResult owner()
        {
            // Fetch the total number of companies
            var totalCompanies = _context.CompanyDetails.Count();

            // Pass the count to the view using ViewBag
            ViewBag.TotalCompanies = totalCompanies;

            var totalemployees = _context.EmpRegisters.Count();
            ViewBag.Totalemplyooes = totalemployees;

           var totalpolicies = _context.Policies.Count();
            ViewBag.Totalpolicies = totalpolicies;


            var approvedPolicies = _context.PolicyRequestDetails
                                   .Where(p => p.Status == "Approved")
                                   .Count();

            // Pass the count to the view
            ViewBag.ApprovedPolicies = approvedPolicies;







            return View();
        }

       



    }
}
