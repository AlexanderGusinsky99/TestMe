using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestMe.BLL
{
    public class Test : IAnswerable
    {
        private List<AbstractQuestion> _listOfQuestions;
        private List<Answer.Answer> _listOfAnswers;
        private int Length = 0;
        public string Name { get; set; }
        public Test(string nameOfTest, List<AbstractQuestion> listOfQuestions)
        {
            Name = nameOfTest;
            _listOfQuestions = listOfQuestions;
        }
        public void SetName(string nameOfTest)
        {
            Name = nameOfTest;
        }
        public void AddQuestion(AbstractQuestion question)
        {
            _listOfQuestions.Add(question);
            Length++;
        }
    }
}