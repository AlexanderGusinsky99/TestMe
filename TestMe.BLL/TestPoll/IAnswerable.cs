using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMe.BLL
{
    public interface IAnswerable
    {
        public string Name { get; set; }
        public void SetName(string name);
        public void AddQuestion(AbstractQuestion question);
    }
}