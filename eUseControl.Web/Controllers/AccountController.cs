using eUseControl.BusinessLogic;
using eUseControl.Domain.Entities;
using System.Web.Mvc;
using System.Web.Security;

namespace eUseControl.Web.Controllers
{
     public class AccountController : Controller
     {
          private readonly AuthBL _auth = new AuthBL();

          [HttpGet]
          public ActionResult Login()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Login(string username, string password)
          {
               if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
               {
                    ViewBag.Error = "Username și parola sunt obligatorii";
                    return View();
               }

               var user = _auth.Login(username, password);
               if (user != null)
               {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["LoggedUser"] = user; // Pentru Cart și alte funcționalități
                    return RedirectToAction("Index", "Products");
               }

               ViewBag.Error = "Utilizator sau parolă greșită";
               return View();
          }

          [HttpGet]
          public ActionResult Register()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Register(string username, string password, string email)
          {
               if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
               {
                    ViewBag.Error = "Toate câmpurile sunt obligatorii";
                    return View();
               }

               var success = _auth.Register(new User
               {
                    Username = username,
                    Password = password,
                    Email = email
               });

               if (success)
               {
                    ViewBag.Success = "Cont creat cu succes! Te poți loga acum.";
                    return RedirectToAction("Login");
               }

               ViewBag.Error = "Utilizatorul există deja sau a apărut o eroare";
               return View();
          }

          public ActionResult Logout()
          {
               FormsAuthentication.SignOut();
               Session.Clear(); 
               return RedirectToAction("Login");
          }
     }
}