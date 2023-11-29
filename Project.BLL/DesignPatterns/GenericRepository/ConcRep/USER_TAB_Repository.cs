using Project.BLL.DesignPatterns.GenericRepository.BaseRep;
using Project.BLL.DesignPatterns.GenericRepository.IntRep;
using Project.DAL.Context;
using Project.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.ConcRep
{
    public class USER_TAB_Repository : BaseRepository<USER_TAB>
    {
        public USER_TAB_Repository(MyContext db) : base(db)
        {

        }
    }
}
