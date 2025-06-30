using eUseControl.Domain;
using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

public class CartBL
{
     private readonly eUseControlContext _db = new eUseControlContext();

     public void SaveOrder(int userId, List<CartItem> cartItems)
     {
          var order = new Order
          {
               UserId = userId,
               OrderDate = DateTime.Now,
               TotalPrice = cartItems.Sum(x =>
               {
                    var product = _db.Products.FirstOrDefault(p => p.Id == x.ProductId);
                    return (product != null ? product.Price : 0) * x.Quantity;
               }),
               OrderItems = cartItems.Select(x =>
               {
                    var product = _db.Products.FirstOrDefault(p => p.Id == x.ProductId);
                    return new OrderItem
                    {
                         ProductId = x.ProductId,
                         Quantity = x.Quantity,
                         Subtotal = (product != null ? product.Price : 0) * x.Quantity
                    };
               }).ToList()
          };

          _db.Orders.Add(order);
          _db.SaveChanges();
     }
}
