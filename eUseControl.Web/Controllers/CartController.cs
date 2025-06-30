using eUseControl.Domain;
using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using System.Web.SessionState;

[SessionState(SessionStateBehavior.Required)]
public class CartController : Controller
{
     private readonly eUseControlContext db = new eUseControlContext();

     public ActionResult Index()
     {
          var cart = GetCartFromSession();
          return View(cart);
     }

     [HttpPost]
     public JsonResult PlaceOrder()
     {
          try
          {
               var cart = GetCartFromSession();
               var user = Session["LoggedUser"] as User;

               if (cart == null || !cart.Any())
               {
                    return Json(new { success = false, message = "Coșul este gol!" });
               }

               if (user == null)
               {
                    // Returnează un redirectUrl pentru a forța logarea
                    return Json(new { success = false, message = "Trebuie să fii autentificat pentru a plasa o comandă!", redirectUrl = "/Account/Login" });
               }

               // Validăm că toate produsele din coș încă există în baza de date
               foreach (var cartItem in cart)
               {
                    var product = db.Products.Find(cartItem.Product.Id);
                    if (product == null)
                    {
                         // Golim coșul și informăm utilizatorul că un produs nu mai e disponibil
                         Session["cart"] = null;
                         return Json(new { success = false, message = $"Produsul '{cartItem.Product.Name}' nu mai este disponibil și a fost eliminat din coș. Vă rugăm să verificați coșul." });
                    }
                    // Actualizăm prețul în caz că s-a schimbat de la adăugarea în coș
                    cartItem.Product.Price = product.Price;
               }

               var order = new eUseControl.Domain.Entities.Order
               {
                    UserId = user.Id,
                    OrderDate = DateTime.Now,
                    TotalPrice = 0, // Va fi actualizat mai jos
                    OrderItems = new List<OrderItem>()
               };

               decimal totalPrice = 0;
               foreach (var item in cart)
               {
                    var orderItem = new OrderItem
                    {
                         ProductId = item.Product.Id,
                         Quantity = item.Quantity,
                         // Subtotal este calculat pe baza prețului actual al produsului din coș (care a fost actualizat mai sus)
                         Subtotal = item.Product.Price * item.Quantity,
                         // Nu este necesar să setezi obiectul Product aici, EF o va face prin ProductId la salvare
                         // Product = item.Product
                    };
                    order.OrderItems.Add(orderItem);
                    totalPrice += orderItem.Subtotal;
               }

               order.TotalPrice = totalPrice;

               db.Orders.Add(order);
               db.SaveChanges(); // Salvează comanda și OrderItems

               // Golim coșul din sesiune după ce comanda a fost plasată cu succes
               Session["cart"] = null;

               return Json(new
               {
                    success = true,
                    message = "Comanda a fost plasată cu succes!",
                    orderId = order.Id, // Poți trimite ID-ul comenzii dacă e nevoie
                    redirectUrl = "/Cart/ThankYou" // Redirecționare după notificare
               });
          }
          catch (Exception ex)
          {
               // Loghează excepția (folosește un logger real într-o aplicație de producție)
               System.Diagnostics.Debug.WriteLine("Eroare la PlaceOrder: " + ex.Message);
               return Json(new { success = false, message = "A apărut o eroare la procesarea comenzii. Te rugăm să încerci din nou." });
          }
     }

     public ActionResult ThankYou()
     {
          if (Session["LoggedUser"] == null)
               return RedirectToAction("Login", "Account");
          return View();
     }

     [HttpPost]
     public JsonResult UpdateCartItemQuantity(int productId, int quantity)
     {
          try
          {
               var cart = GetCartFromSession();
               if (cart == null || !cart.Any())
               {
                    return Json(new { success = false, message = "Coșul este gol." });
               }

               var itemToUpdate = cart.FirstOrDefault(item => item.Product.Id == productId);
               if (itemToUpdate == null)
               {
                    return Json(new { success = false, message = "Produsul nu a fost găsit în coș." });
               }

               bool isRemoved = false;
               string message = "";

               if (quantity <= 0)
               {
                    cart.Remove(itemToUpdate);
                    isRemoved = true;
                    message = "Produsul a fost eliminat din coș!";
               }
               else
               {
                    // Validare cantitate
                    if (quantity > 99)
                    {
                         quantity = 99; // Forțăm la maxim
                         message = "Cantitatea maximă este 99. Cantitatea a fost setată la 99.";
                    }
                    itemToUpdate.Quantity = quantity;
                    message = "Cantitatea a fost actualizată!";
               }

               // Salvăm coșul actualizat în sesiune
               Session["cart"] = cart;

               // Calculăm totalurile
               decimal newGrandTotal = cart.Sum(item => item.Quantity * item.Product.Price);
               // newGrandTotal = Math.Round(newGrandTotal, 2); // Rotunjire pentru precizie monetară dacă e nevoie

               decimal newItemTotal = isRemoved ? 0 : itemToUpdate.Quantity * itemToUpdate.Product.Price;
               // newItemTotal = Math.Round(newItemTotal, 2); // Rotunjire

               return Json(new
               {
                    success = true,
                    message = message,
                    newQuantity = isRemoved ? 0 : itemToUpdate.Quantity,
                    newItemTotal = newItemTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("ro-RO")),
                    newGrandTotal = newGrandTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("ro-RO")),
                    isRemoved = isRemoved,
                    cartItemCount = cart.Count // Numărul total de produse diferite în coș
               });
          }
          catch (Exception ex)
          {
               System.Diagnostics.Debug.WriteLine("Eroare la UpdateCartItemQuantity: " + ex.Message);
               return Json(new { success = false, message = "A apărut o eroare la actualizarea coșului." });
          }
     }

     // Metodă helper pentru a obține coșul din sesiune
     private List<CartItem> GetCartFromSession()
     {
          var cart = Session["cart"] as List<CartItem>;
          if (cart == null)
          {
               cart = new List<CartItem>();
               Session["cart"] = cart;
          }
          return cart;
     }

     protected override void Dispose(bool disposing)
     {
          if (disposing)
          {
               db.Dispose();
          }
          base.Dispose(disposing);
     }
}