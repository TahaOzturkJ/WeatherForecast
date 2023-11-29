using Project.ENTITY.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITY.Models
{
    public class USER_LOG_TAB : BaseEntity
    {
        public string IpAddress { get; set; }

        public LogYon Yon { get; set; }


        //Relational Properties
        public virtual USER_TAB USER_TAB { get; set; }

        [ForeignKey("USER_TAB")]
        public int UserId { get; set; }


    }
}
