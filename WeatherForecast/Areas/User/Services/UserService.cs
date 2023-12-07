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

        public UserVM EditUserCity(int id, WeatherInfoVM wiVM, [FromServices] IToastNotification toast)
        {
            UserVM user = new UserVM();

            user.USER_TAB = _utRep.Find(id);

            if (user != null)
            {
                user.USER_TAB.DefaultCityName = wiVM.weather_Info_Tab.CityName;

                _utRep.Update(user.USER_TAB);

                toast.AddSuccessToastMessage("Şehir bilgisi başarıyla güncellendi.", new ToastrOptions { Title = "Başarılı!" });
            }

            return user;
        }

        public UserVM EditUserProfile(int id, UserProfileVM upVM, [FromServices] IToastNotification toast)
        {
            UserVM user = new UserVM();

            user.USER_TAB = _utRep.Find(id);

            if (user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(upVM.Password, user.USER_TAB.HashedPassword);

                if (isValidPassword)
                {
                    user.USER_TAB.Name = upVM.Name;

                    user.USER_TAB.Email = upVM.Email;

                    user.USER_TAB.DefaultCityName = upVM.DefaultCityName;

                    _utRep.Update(user.USER_TAB);

                    toast.AddSuccessToastMessage("Profil bilgileri başarıyla güncellendi.", new ToastrOptions { Title = "Başarılı!" });

                    return user;
                }

                toast.AddErrorToastMessage("Lütfen şifrenizi doğru girin.", new ToastrOptions { Title = "Başarısız!" });

            }

            return user;
        }

        public UserVM ChangeUserPassword(int id, UserSecurityVM usVM, [FromServices] IToastNotification toast)
        {
            UserVM user = new UserVM();

            user.USER_TAB = _utRep.Find(id);

            if (user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(usVM.Password, user.USER_TAB.HashedPassword);

                if (isValidPassword)
                {
                    user.USER_TAB.HashedPassword = BCrypt.Net.BCrypt.HashPassword(usVM.NewPassword);

                    _utRep.Update(user.USER_TAB);

                    toast.AddSuccessToastMessage("Şifre başarıyla güncellendi.", new ToastrOptions { Title = "Başarılı!" });

                    return user;
                }

                toast.AddErrorToastMessage("Lütfen şifrenizi doğru girin.", new ToastrOptions { Title = "Başarısız!" });

            }

            return user;
        }

        public UserVM GetUserInfos(int id)
        {
            UserVM user = new UserVM();

            user.USER_TAB = _utRep.Find(id);

            return user;
        }
    }
}
