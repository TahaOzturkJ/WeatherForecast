using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.DAL.Context;
using Project.ENTITY.Models;
using WeatherForecast.Areas.User.Models;
using WeatherForecast.Areas.User.IServices;

namespace WeatherForecast.Areas.User.Services
{
    public class UserService : IUserService
    {
        private readonly USER_TAB_Repository _utRep;

        public UserService(MyContext db)
        {
            _utRep = new USER_TAB_Repository(db);
        }

        public USER_TAB EditUserCity(int id, WeatherInfoVM wiVM, [FromServices] IToastNotification toast)
        {
            USER_TAB user = _utRep.Find(id);

            if (user != null)
            {
                user.DefaultCityName = wiVM.weather_Info_Tab.CityName;

                _utRep.Update(user);

                toast.AddSuccessToastMessage("Şehir bilgisi başarıyla güncellendi.", new ToastrOptions { Title = "Başarılı!" });
            }

            return user;
        }

        public USER_TAB EditUserProfile(int id, UserProfileVM upVM, [FromServices] IToastNotification toast)
        {
            USER_TAB user = _utRep.Find(id);

            if (user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(upVM.Password, user.HashedPassword);

                if (isValidPassword)
                {
                    user.Name = upVM.Name;

                    user.Email = upVM.Email;

                    user.DefaultCityName = upVM.DefaultCityName;

                    _utRep.Update(user);

                    toast.AddSuccessToastMessage("Profil bilgileri başarıyla güncellendi.", new ToastrOptions { Title = "Başarılı!" });

                    return user;
                }

                toast.AddErrorToastMessage("Lütfen şifrenizi doğru girin.", new ToastrOptions { Title = "Başarısız!" });

            }

            return user;
        }

        public USER_TAB ChangeUserPassword(int id, UserSecurityVM usVM, [FromServices] IToastNotification toast)
        {
            USER_TAB user = _utRep.Find(id);

            if (user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(usVM.Password, user.HashedPassword);

                if (isValidPassword)
                {
                    user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(usVM.NewPassword);

                    _utRep.Update(user);

                    toast.AddSuccessToastMessage("Şifre başarıyla güncellendi.", new ToastrOptions { Title = "Başarılı!" });

                    return user;
                }

                toast.AddErrorToastMessage("Lütfen şifrenizi doğru girin.", new ToastrOptions { Title = "Başarısız!" });

            }

            return user;
        }

        public USER_TAB GetUserInfos(int id)
        {
            USER_TAB user = _utRep.Find(id);

            return user;
        }
    }
}
