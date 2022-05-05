using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TestMe.BLL
{
    public class UserMe
    {
        public long UserID;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserMe(string firstName, string lastName, long id)
        {
            FirstName = firstName;
            LastName = lastName;
            UserID = id;
        }

    }
}
