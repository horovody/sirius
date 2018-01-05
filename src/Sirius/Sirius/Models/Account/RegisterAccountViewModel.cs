using System.ComponentModel.DataAnnotations;

namespace Sirius.Models.Account
{
    public class RegisterAccountViewModel
    {
        [Display(Name = "Username")]
        [Required]
        [StringLength(512)]
        public string UserName { get; set; }

        [Display(Name = "Username")]
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Username")]
        [Required]
        [StringLength(512)]
        public string GivenName { get; set; }
    }
}
