using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TestMe.BLL;

namespace TestController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> _crntAnswers = new List<string>();
        public List<AbstractQuestion> _crntQuestions = new List<AbstractQuestion>();
        public int _indexOfRigthAnswer = -1;
        private List<Test> _listOfTests = new List<Test>();
        private TelegramBot _telegramBot;
        private const string _token = "5214418897:AAGMzUpDI8mf2cVJ0S7kFGa_QheT0LYonMQ";
        private DispatcherTimer _timer;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            RadioButton answer = new RadioButton();
            answer.Content = TextBoxAnswer.Text;
            string answerText = TextBoxAnswer.Text;
            _crntAnswers.Add(answerText);
            TextBoxAnswer.Text = "Enter answer";
            WrapPanelAnswers.Children.Add(answer);
        }
        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string textOfQuestion = TextBoxTextOfQuestion.Text;
            TextBoxTextOfQuestion.Text = "Enter text of question";
            AbstractQuestion question = new OneAnswerQuestion(textOfQuestion, _crntAnswers, _indexOfRigthAnswer);
            _crntQuestions.Add(question);
            WrapPanelAnswers.Children.Clear();
        }
        private void ButtonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            string nameOfTest = TextBoxNameOfTest.Text;
            _listOfTests.Add(new Test(nameOfTest, _crntQuestions));
            TextBoxNameOfTest.Text = "Enter name of text";
            _crntQuestions = new List<AbstractQuestion>();
            WrapPanelAnswers.Children.Clear();
            TextBoxTextOfQuestion.Text = "Enter text of question";

        }
        private void ButtonTestCreator_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Visible;
        }
        private void ButtonTGCheckChat_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Hidden;
        }
    }
}
