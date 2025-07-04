﻿using eUseControl.Domain.Entities; // Asigură-te că Product și Order sunt în același namespace

namespace eUseControl.Domain.Entities
{
     public class OrderItem
     {
          public int Id { get; set; }
          public int OrderId { get; set; }
          public int ProductId { get; set; }
          public int Quantity { get; set; }
          public decimal Subtotal { get; set; }

          public virtual Product Product { get; set; }
          public virtual Order Order { get; set; }
     }
}