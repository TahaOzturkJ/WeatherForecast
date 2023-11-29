using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Areas.Auth.Models
{
    public class UserAuthModel
    {
        [Display(Name="Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Giriniz")]
        public string Username { get; set; }

        [Display(Name ="Şifre")]
        [Required(ErrorMessage = "Şifreyi giriniz")]
        public string Password { get; set; }

        [Display(Name ="Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler uyumlu değil")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifreyi giriniz")]
        public string Name { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "E-Posta Adresini Giriniz")]
        public string Email { get; set; }

        [Display(Name = "Şehir Adı")]
        [Required(ErrorMessage = "Şehir Adını Giriniz")]
        public string DefaultCityName { get; set; }
    }
}
