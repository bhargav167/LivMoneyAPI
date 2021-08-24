using System.ComponentModel.DataAnnotations;

namespace LivMoneyAPI.Model.Authentication {
    public class LoginModal {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}