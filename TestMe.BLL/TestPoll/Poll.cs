using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    internal class Poll : IAnswerable
    {
        public string Name { get; set; }

        public void SetName(string nameOfPoll)
        {
            Name = nameOfPoll;
        }
        public void AddQuestion(AbstractQuestion quetion)
        {
        }
    }
}
