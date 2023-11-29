using Project.ENTITY.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITY.Models
{
    public class USER_TAB : BaseEntity
    {
        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public string Name { get; set; }

        public string DefaultCityName { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public bool IsLocked { get; set; }

        public int FailedLoginAttempts { get; set; }

        public DateTime? LockoutEndDate { get; set; }


        //Relational Properties
        public virtual ICollection<USER_LOG_TAB> USER_LOG_TABs { get; set; }


        public USER_TAB()
        {
            Role = Role.SonKullanici;
        }
    }
}
