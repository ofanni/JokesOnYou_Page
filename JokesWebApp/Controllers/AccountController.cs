using JokesWebApp.Data;
using JokesWebApp.Models.Domain;
using JokesWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AccountController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]

        public  IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                 
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                // Check if username already exists
                if (applicationDbContext.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(model);
                }


                var hashedPassword = HashPassword(model.Password);

                var user = new User
                {
                    Username = model.Username,
                    Password = hashedPassword,
                };

                applicationDbContext.Users.Add(user);
                applicationDbContext.SaveChanges();

                HttpContext.Session.SetString("Username", model.Username);
                return RedirectToAction("Index", "Home");
            }

            return View("Register");
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(model.Password);

                var user = applicationDbContext.Users
                    .SingleOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

                if (user != null)
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
