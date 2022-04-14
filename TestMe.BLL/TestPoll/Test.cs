using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    public class Test : IAnswerable
    {
        private List<AbstractQuetion> _listOfQuestions;
        private int Lenght = 0;
        public string Name { get; set; }

        public void SetName(string nameOfTest)
        {
            Name = nameOfTest;
        }
        public void AddQuestion(AbstractQuetion quetion)
        {
            _listOfQuestions.Add(quetion);
            Lenght++;
        }
    }
}