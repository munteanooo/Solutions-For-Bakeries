using System.Web.Mvc;
using eUseControl.Web.Models.Users;

namespace eUseControl.Web.Controllers
{
     public class LoginController : Controller
     {
          // GET: Login
          [HttpGet]
          public ActionResult Login()
          {
               return View(new UserLogin());
          }

          // POST: Login
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Login(UserLogin user)
          {
               if (ModelState.IsValid)
               {
                    // Exemplu simplu de autentificare hardcodată
                    if (user.Email == "test@gmail.com" && user.Password == "123456")
                    {
                         // Poți salva ceva în sesiune
                         Session["User"] = user.Email;

                         // Redirect către dashboard sau home
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", "Email sau parolă greșită.");
                    }
               }

               return View(user);
          }

          // GET: Register
          public ActionResult Register()
          {
               return View();
          }

          // GET: RecoverPassword
          public ActionResult RecoverPassword()
          {
               return View();
          }
     }
}
