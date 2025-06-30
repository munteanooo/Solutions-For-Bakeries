using eUseControl.Domain;
using eUseControl.Domain.Entities; // Asigură-te că ai acest using pentru a accesa Order, User, Product, OrderItem
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; // <-- IMPORTANT: Adaugă acest using pentru metoda .Include()

namespace eUseControl.Web.Controllers
{
     public class AdminController : Controller
     {
          private readonly eUseControlContext db = new eUseControlContext();

          public ActionResult Orders()
          {
               var orders = db.Orders
                   // Încărcăm proprietatea de navigare User direct din Order
                   .Include(o => o.User)
                   // Încărcăm colecția OrderItems, și pentru fiecare OrderItem, încărcăm Product-ul asociat
                   .Include(o => o.OrderItems.Select(oi => oi.Product))
                   .ToList();

               return View(orders);
          }

          // Aici poți adăuga alte acțiuni specifice pentru zona de administrare

          protected override void Dispose(bool disposing)
          {
               if (disposing)
               {
                    db.Dispose();
               }
               base.Dispose(disposing);
          }
     }
}