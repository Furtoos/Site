using Marvel_of_the_Universe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Marvel_of_the_Universe.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            UserContext db = new UserContext();
            User user = db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else { return View(user); }
             
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Marvel");
                }
                else
                {
                    ModelState.AddModelError("", "Пользыватель с таким логином и паролем нет");
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name);
                }
                if (user == null)
                {
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new User { Email = model.Name, Password = model.Password, Age = model.Age });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Marvel");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользыватель с таким логином уже существует");
                }
            }
            return View(model);
        }

    }
}