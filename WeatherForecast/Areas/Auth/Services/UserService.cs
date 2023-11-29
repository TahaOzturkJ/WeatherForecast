using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.DAL.Context;
using Project.ENTITY.Enums;
using Project.ENTITY.Models;
using System.Security.Claims;
using WeatherForecast.Areas.Auth.IServices;
using WeatherForecast.Areas.Auth.Models;

namespace WeatherForecast.Areas.Auth.Services
{
    public class UserService : IUserService
    {
        private readonly USER_TAB_Repository _uRep;
        private readonly USER_LOG_TAB_Repository _uLogRep;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(MyContext db, IHttpContextAccessor httpContextAccessor)
        {
            _uRep = new USER_TAB_Repository(db);
            _uLogRep = new USER_LOG_TAB_Repository(db);
            _httpContextAccessor = httpContextAccessor;
        }

        public USER_LOG_TAB CreateLog(USER_TAB user, LogYon yon)
        {
            USER_LOG_TAB ult = new USER_LOG_TAB();

            ult.USER_TAB = user;
            ult.Yon = yon;
            ult.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            _uLogRep.Add(ult);

            return ult;
        }

        public List<USER_TAB> GetAllUsers()
        {
            return _uRep.GetAll();
        }

        public async Task<USER_TAB> LoginAsync(UserAuthModel uam, [FromServices] IToastNotification toast)
        {
            var user = _uRep.FirstOrDefault(x => x.Username == uam.Username);

            if (user != null)
            {
                if (user.IsLocked && user.LockoutEndDate > DateTime.Now)
                {
                    toast.AddErrorToastMessage("Güvenliğiniz için hesabınız bir süreliğine engellendi.", new ToastrOptions { Title = "Başarısız!" });

                    return null;
                }

                bool isValidPassword = BCrypt.Net.BCrypt.Verify(uam.Password, user.HashedPassword);

                if (isValidPassword)
                {
                    user.IsLocked = false;
                    user.FailedLoginAttempts = 0;
                    user.LockoutEndDate = null;

                    _uRep.Update(user);

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authProperties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                    };

                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    CreateLog(user, LogYon.Giriş);

                    return user;
                }
                else
                {
                    user.FailedLoginAttempts++;

                    if (user.FailedLoginAttempts == 3)
                    {
                        user.IsLocked = true;
                        user.LockoutEndDate = DateTime.Now.AddMinutes(1);
                        user.FailedLoginAttempts = 0;
                    }

                    _uRep.Update(user);

                    toast.AddErrorToastMessage("Kullanıcı adı veya şifre hatalı.", new ToastrOptions { Title = "Başarısız!" });
                }
            }

            return null;
        }

        public USER_TAB Register(UserAuthModel uam, [FromServices] IToastNotification toast)
        {
            if (!_uRep.Any(x => x.Username == uam.Username))
            {
                USER_TAB user = new USER_TAB();

                user.Username = uam.Username;

                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(uam.Password);

                user.Email = uam.Email;

                user.DefaultCityName = uam.DefaultCityName;

                user.Name = uam.Name;

                _uRep.Add(user);

                toast.AddSuccessToastMessage("Kullanıcı Oluşturuldu!", new ToastrOptions { Title = "Başarılı!" });

                return user;
            }

            toast.AddErrorToastMessage("Kayıt yapılamadı.", new ToastrOptions { Title = "Başarısız!" });

            return null;
        }

        public async Task<USER_TAB> LogoutAsync(int userId,[FromServices] IToastNotification toast)
        {
            USER_TAB user = _uRep.Find(userId);

            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            toast.AddSuccessToastMessage("Başarıyla çıkış yaptınız.", new ToastrOptions { Title = "Başarılı!" });

            CreateLog(user, LogYon.Çıkış);

            return null;
        }
    }
}
