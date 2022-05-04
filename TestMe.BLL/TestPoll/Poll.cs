using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    public class Poll : IAnswerable
    {
        private List<AbstractQuestion> _listOfQuestions;
        private int Length = 0;
        public string Name { get; set; }

        public Poll(string nameOfPoll, List<AbstractQuestion> listOfQuestions)
        {
            Name = nameOfPoll;
            _listOfQuestions = listOfQuestions;
        }


        public void SetName(string nameOfPoll)
        {
            Name = nameOfPoll;
        }
        public void AddQuestion(AbstractQuestion question)
        {
        }
    }
}
