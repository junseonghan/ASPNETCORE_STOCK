using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HDnet.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private DBContext.ApplicationDbContext dbContext = null;
        public AccountController(DBContext.ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View("SignUp", new Users());
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(Users user)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            dbContext.Account.Add(user);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Id, string password)
        {
            var account = checkAccount(Id, password);
            if(account == null)
            {
                ViewBag.error = "Invalid";
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetString("username", Id);
                Console.WriteLine(HttpContext.Session.GetString("username"));
                //return View("Success");
                return RedirectToAction("Index", "Dash");
            }
        }

        private Users checkAccount(string Id, string password)
        {
            var account = dbContext.Account.SingleOrDefault(x => x.Id.Equals(Id));
            if(account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.password))
                {
                    return account;
                }
            }
            return null;
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

    }
}