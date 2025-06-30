using eUseControl.Domain;
using eUseControl.Domain.Entities;
using System;
using System.Linq;

namespace eUseControl.BusinessLogic
{
     public class AuthBL
     {
          public bool Register(User newUser)
          {
               using (var db = new eUseControlContext())
               {
                    bool exists = db.Users.Any(u => u.Username == newUser.Username);
                    if (exists) return false;

                    newUser.Password = HashPassword(newUser.Password);
                    newUser.CreatedAt = DateTime.Now;

                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return true;
               }
          }

          public User Login(string username, string password)
          {
               using (var db = new eUseControlContext())
               {
                    string hashed = HashPassword(password);
                    return db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashed);
               }
          }

          private string HashPassword(string password)
          {
               using (var sha = System.Security.Cryptography.SHA256.Create())
               {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                    var hash = sha.ComputeHash(bytes);
                    return Convert.ToBase64String(hash);
               }
          }
     }
}
