using Microsoft.EntityFrameworkCore;
using Project.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public virtual DbSet<USER_TAB> USER_TABs { get; set; } = null!;

        public virtual DbSet<USER_LOG_TAB> USER_LOG_TABs { get; set; } = null!;

        public virtual DbSet<Weather_Info_Tab> Weather_Info_Tabs { get; set; } = null!;
    }
}
