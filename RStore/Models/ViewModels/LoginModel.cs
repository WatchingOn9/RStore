using System.ComponentModel.DataAnnotations;

namespace RStore.Models.ViewModels {
    public class LoginModel {
        [Required]
        public string Name { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}