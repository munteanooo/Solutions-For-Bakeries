using System;
using System.Collections.Generic;

namespace eUseControl.Domain.Entities
{
     public class Order

     {
          public int Id { get; set; }

          public int UserId { get; set; } // Foreign key pentru User

          public DateTime OrderDate { get; set; }

          public decimal TotalPrice { get; set; } // Totalul comenzii

          // NOU: Proprietatea de navigare către User

          public virtual User User { get; set; }

          public virtual ICollection<OrderItem> OrderItems { get; set; }

     }
}