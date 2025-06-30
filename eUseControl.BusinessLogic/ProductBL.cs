using eUseControl.Domain;
using eUseControl.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
public class ProductBL
{
     private readonly eUseControlContext _db = new eUseControlContext();

     public List<Product> GetAllProducts()
     {
          return _db.Products.ToList();
     }

     public Product GetProductById(int id)
     {
          return _db.Products.FirstOrDefault(p => p.Id == id);
     }
}
