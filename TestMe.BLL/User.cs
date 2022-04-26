using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TestMe.BLL
{
    public class User
    {
        //private long _userID; (хранить ID пользователя)
        public string UserName { get; set; }
        public Chat Chat { get; set; }
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
        //public string Name { get; set; }
        //public Chat Chat { get; set; }
        //public string Test { get; set; }
        //public int CrntQuestIndex { get; set; }
        //public List<string> Answers { get; set; } = new List<string>();
        
    }
}
