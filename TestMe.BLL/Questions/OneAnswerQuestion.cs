using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    public class OneAnswerQuestion : AbstractQuestion // Выбор одного ответа
    {
        //Поля или свойства? Пока не ясно, главное, чтобы работало, но потом поправить нужно обязательно
        public string TextOfQuestion { get; set; }
        public List<string> _listOfAnswers;
        public int _indexOfRightAnswer;
        public OneAnswerQuestion(string textOfQuestion, List<string> listOfAnswers, int indexOfRightAnswer)
        {
            TextOfQuestion = textOfQuestion;
            List<string> _listOfAnswers = listOfAnswers;
            _indexOfRightAnswer = indexOfRightAnswer;
        }
    }
}