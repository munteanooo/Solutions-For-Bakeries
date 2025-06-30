using eUseControl.Domain;
using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web.SessionState;

namespace eUseControl.Web.Controllers
{
     [SessionState(SessionStateBehavior.Required)]
     public class ProductsController : Controller
     {
          private readonly eUseControlContext db = new eUseControlContext();

          public ActionResult Index()
          {
               var products = db.Products.ToList();

               // Dacă nu ai produse, adaugă câteva pentru test (doar dacă baza de date e goală)
               if (!products.Any())
               {
                    var testProducts = new List<Product>
                {
                    // **********************************************
                    // DOAR PRODUSELE PANIFICATE SUNT AICI ACUM
                    // **********************************************
                    new Product
                    {
                        Name = "Pâine de Casă",
                        Description = "Pâine artizanală, coaptă zilnic, cu crustă crocantă și miez moale.",
                        Price = 8.50m, // MDL lei
                        Grams = 750,
                        Ingredients = "Făină de grâu, apă, maia naturală, sare de mare",
                        Expiration = DateTime.Now.AddDays(2)
                    },
                    new Product
                    {
                        Name = "Croissant cu Unt",
                        Description = "Croissant franțuzesc, preparat cu unt 82%, pufos și aromat.",
                        Price = 12.00m, // MDL lei
                        Grams = 80,
                        Ingredients = "Făină de grâu, unt, lapte, drojdie, zahăr, sare",
                        Expiration = DateTime.Now.AddDays(1)
                    },
                    new Product
                    {
                        Name = "Baguette Tradițională",
                        Description = "Baguette subțire și lungă, cu coaja aurie și interior aerat, perfectă pentru sandvișuri.",
                        Price = 7.00m, // MDL lei
                        Grams = 250,
                        Ingredients = "Făină de grâu, apă, drojdie, sare",
                        Expiration = DateTime.Now.AddDays(1)
                    },
                    new Product
                    {
                        Name = "Chifle Integrale",
                        Description = "Set de 4 chifle din făină integrală, bogate în fibre și semințe.",
                        Price = 6.50m, // MDL lei
                        Grams = 240, // 4 * 60g
                        Ingredients = "Făină integrală de grâu, semințe de floarea-soarelui, in, susan, apă, drojdie, sare",
                        Expiration = DateTime.Now.AddDays(2)
                    },
                    new Product
                    {
                        Name = "Pâine cu Semințe",
                        Description = "Pâine consistentă, îmbogățită cu un mix de semințe nutritive, ideală pentru un stil de viață sănătos.",
                        Price = 10.20m, // MDL lei
                        Grams = 600,
                        Ingredients = "Făină de secară, semințe de dovleac, floarea-soarelui, in, ovăz, sare, drojdie",
                        Expiration = DateTime.Now.AddDays(4)
                    },
                    new Product
                    {
                        Name = "Covrigi Proaspeți",
                        Description = "Covrigi fierbinți și aromați, presărați cu sare și semințe de mac.",
                        Price = 3.50m, // MDL lei
                        Grams = 100,
                        Ingredients = "Făină de grâu, apă, sare, zahăr, drojdie, mac",
                        Expiration = DateTime.Now.AddHours(12)
                    }
                };

                    foreach (var product in testProducts)
                    {
                         db.Products.Add(product);
                    }

                    try
                    {
                         db.SaveChanges();
                         products = db.Products.ToList();
                    }
                    catch (Exception ex)
                    {
                         System.Diagnostics.Debug.WriteLine("Eroare la salvarea produselor de test: " + ex.Message);
                         products = new List<Product>();
                         ViewBag.Error = "Nu s-au putut încărca produsele de test. Verificați configurația bazei de date.";
                    }
               }

               // Nu mai trimitem ViewBag.CartItemCount aici, deoarece iconița dinamică va fi eliminată
               return View(products);
          }

          [HttpPost]
          public ActionResult AddToCart(int id)
          {
               var productToAdd = db.Products.Find(id);
               if (productToAdd == null)
               {
                    // Putem face o redirecționare la o pagină de eroare sau la Index
                    return RedirectToAction("Index", new { error = "Produsul nu a fost găsit." });
               }

               List<CartItem> cart = Session["cart"] as List<CartItem>;
               if (cart == null)
               {
                    cart = new List<CartItem>();
                    Session["cart"] = cart;
               }

               CartItem existingItem = cart.FirstOrDefault(item => item.Product.Id == id);
               if (existingItem != null)
               {
                    existingItem.Quantity++;
               }
               else
               {
                    cart.Add(new CartItem { Product = productToAdd, Quantity = 1 });
               }

               return RedirectToAction("Index", "Cart");
          }


          private int GetCartItemCount()
          {
               List<CartItem> cart = Session["cart"] as List<CartItem>;
               if (cart == null)
               {
                    return 0;
               }
               return cart.Sum(item => item.Quantity);
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
}