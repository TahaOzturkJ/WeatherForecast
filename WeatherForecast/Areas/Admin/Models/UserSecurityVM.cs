using Project.ENTITY.Models;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Areas.Admin.Models
{
    public class UserSecurityVM
    {
        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Yeni Şifreyi giriniz")]
        public string NewPassword { get; set; }

        [Display(Name = "Mevcut Şifre")]
        [Required(ErrorMessage = "Şifreyi giriniz")]
        public string Password { get; set; }

        [Display(Name = "Mevcut Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler uyumlu değil")]
        public string ConfirmPassword { get; set; }

        public USER_TAB? USER_TAB { get; set; }

    }
}
