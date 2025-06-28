using EPROJECT.Models;
using EPROJECT.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EPROJECT.Controllers
{
    public class AccountController : Controller
    {
        insurance_companyContext _context;
        public AccountController(insurance_companyContext _context)
        {
            this._context = _context;
        }

        public IActionResult Login(string returnUrl="/")
        {

            LoginViewModel login = new LoginViewModel();
            login.Returnurl= returnUrl;
            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult>  Login(LoginViewModel loginView)
        {
            var emp = _context.EmpRegisters.FirstOrDefault(e=> e.Email == loginView.Email && e.Password == loginView.Password);

            if (emp != null) {

                var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,emp.Empid.ToString() ),
                    new Claim(ClaimTypes.Name,emp.Username),
                    new Claim(ClaimTypes.Email,emp.Email),
                    new Claim(ClaimTypes.Role,emp.Designation)
                };
                var identity = new ClaimsIdentity(claim,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal,new AuthenticationProperties
                {
                    IsPersistent= loginView.RememberLogin,
                });




                if (loginView.Returnurl == "/" && emp.Designation == "Admin")
                {
                    return RedirectToAction("owner", "admin");
                }

                return LocalRedirect(loginView.Returnurl);
            }

            else
            {
                TempData["Error"] = "Invalid User";
            }

            return View();
        }
        public async Task<IActionResult> Signout()
        {

           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if the email exists
            var user = _context.EmpRegisters.FirstOrDefault(e => e.Email == model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid email address.";
                return View();
            }

            // Validate if the new password matches confirm password
            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["Error"] = "New Password and Confirm Password do not match.";
                return View();
            }

            // Update the password
            user.Password = model.NewPassword;
            _context.SaveChanges();

            TempData["Message"] = "Password changed successfully!";
            return RedirectToAction("Index", "insurance");
        }


    }
}
