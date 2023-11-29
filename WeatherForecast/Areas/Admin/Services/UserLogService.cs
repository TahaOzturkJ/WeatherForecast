using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.DAL.Context;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Services
{
    public class UserLogService : IUserLogService
    {
        private readonly USER_LOG_TAB_Repository _ulRep;

        public UserLogService(MyContext db)
        {
            _ulRep = new USER_LOG_TAB_Repository(db);
        }

        public UserLogVM GetFilteredUserLogs(string Name, string Email, [FromServices] IToastNotification toast)
        {
            UserLogVM userLog = new UserLogVM();

            userLog.USER_LOG_TABs = _ulRep.GetAllActiveLogsWithUsers();

            if (!string.IsNullOrEmpty(Name))
            {
                userLog.USER_LOG_TABs = userLog.USER_LOG_TABs.Where(x => x.USER_TAB.Name == Name);

                if (!string.IsNullOrEmpty(Email))
                {
                    userLog.USER_LOG_TABs = userLog.USER_LOG_TABs.Where(x => x.USER_TAB.Email == Email);
                }

                toast.AddSuccessToastMessage("Filtrelenmiş kullanıcı kayıtları başarı ile getirildi.", new ToastrOptions { Title = "Başarılı!" });

                return userLog;
            }

            if (!string.IsNullOrEmpty(Email))
            {
                userLog.USER_LOG_TABs = userLog.USER_LOG_TABs.Where(x => x.USER_TAB.Email == Email);

                toast.AddSuccessToastMessage("Filtrelenmiş kullanıcı kayıtları başarı ile getirildi.", new ToastrOptions { Title = "Başarılı!" });

                return userLog;
            }


            toast.AddErrorToastMessage("Filtrelenmiş kullanıcı kayıtları getirilemedi.", new ToastrOptions { Title = "Başarısız!" });

            return userLog;

        }

        public UserLogVM GetUserLogs(UserLogVM userLog)
        {
            userLog.USER_LOG_TABs = _ulRep.GetAllActiveLogsWithUsers();

            return userLog;
        }
    }
}
