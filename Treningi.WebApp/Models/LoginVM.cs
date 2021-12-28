using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Treningi.WebApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wyamgana...")]
        [Display(Name = "Nazwa Użytkownika")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane...")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
