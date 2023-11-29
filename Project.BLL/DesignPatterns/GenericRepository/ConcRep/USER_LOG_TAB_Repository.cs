using Microsoft.EntityFrameworkCore;
using Project.BLL.DesignPatterns.GenericRepository.BaseRep;
using Project.DAL.Context;
using Project.ENTITY.Enums;
using Project.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.ConcRep
{
    public class USER_LOG_TAB_Repository : BaseRepository<USER_LOG_TAB>
    {
        MyContext _db;

        public USER_LOG_TAB_Repository(MyContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<USER_LOG_TAB> GetAllActiveLogsWithUsers()
        {
            return _db.Set<USER_LOG_TAB>().Include(x => x.USER_TAB).Where(x => x.Status != DataStatus.Deleted).ToList();
        }
    }
}
