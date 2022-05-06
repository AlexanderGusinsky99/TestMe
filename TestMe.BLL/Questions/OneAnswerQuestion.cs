using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    public class OneAnswerQuestion : AbstractQuestion // Выбор одного ответа
    {
        
        public string TextOfQuestion { get; set; }
        public int _indexOfRightAnswer;
        public OneAnswerQuestion(string textOfQuestion, List<string> listOfAnswers, int indexOfRightAnswer)
        {
            TextOfQuestion = textOfQuestion;
            List<string> _listOfAnswers = listOfAnswers;
            _indexOfRightAnswer = indexOfRightAnswer;
        }
        public OneAnswerQuestion()
        {

        }


    }
}