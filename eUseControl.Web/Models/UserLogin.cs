using System.ComponentModel.DataAnnotations;

namespace eUseControl.Web.Models.Users
{
     public class UserLogin
     {
          [Required(ErrorMessage = "Emailul este obligatoriu")]
          [EmailAddress(ErrorMessage = "Format email invalid")]
          public string Email { get; set; }

          [Required(ErrorMessage = "Parola este obligatorie")]
          public string Password { get; set; }
     }
}
