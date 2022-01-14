using System.ComponentModel.DataAnnotations;

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
