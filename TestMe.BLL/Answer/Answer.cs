using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestMe.BLL.Answer
{
    public class Answer
    {
        public string TextOfAnswer { get; set; }
        public long Id { get; set; }
        public Answer()
        { }
        public Answer(string textOfAnswer)
        {
            TextOfAnswer = textOfAnswer;
        }
    }
}
