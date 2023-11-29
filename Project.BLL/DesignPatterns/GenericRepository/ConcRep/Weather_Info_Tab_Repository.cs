using Project.BLL.DesignPatterns.GenericRepository.BaseRep;
using Project.DAL.Context;
using Project.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.ConcRep
{
    public class Weather_Info_Tab_Repository : BaseRepository<Weather_Info_Tab>
    {
        public Weather_Info_Tab_Repository(MyContext db) : base(db)
        {

        }
    }
}
