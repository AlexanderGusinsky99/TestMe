using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    public class User
    {
        //private long _userID; (хранить ID пользователя)
        public string UserName { get; set; }
        public string RealName
        {
            get
            {
                return RealName;
            }
            set
            {
                RealName = value;
            }
        }
        public User()
        { }
    }
}
