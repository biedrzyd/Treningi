using System.ComponentModel.DataAnnotations;

namespace Treningi.WebApp.Models
{
    public class CreateRoleVM
    {
        [Required]
        public string RoleName { get; set; }
    }
}
