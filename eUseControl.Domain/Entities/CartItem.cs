using eUseControl.Domain.Entities;

namespace eUseControl.Domain.Entities
{

     public class CartItem
     {
          public Product Product { get; set; }
          public string Name { get; set; }     // Nume produs
          public decimal Price { get; set; }   // Preț pe bucată
          public int Quantity { get; set; }    // Cantitate
          public int ProductId { get; set; }
     }


}
