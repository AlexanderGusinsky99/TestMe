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
        private void ButtonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            string nameOfTest = TextBoxNameOfTest.Text;
            _listOfTests.Add(new Test(nameOfTest, _crntQuestions));
            TextBoxNameOfTest.Text = "Enter name of text";
            _crntQuestions = new List<AbstractQuestion>();
            WrapPanelAnswers.Children.Clear();
            TextBoxTextOfQuestion.Text = "Enter text of question";
            _answerCounter = 1;

        }
        private void ButtonTestCreator_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Visible;
        }
        private void ButtonTGCheckChat_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Hidden;
        }


        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            
            string textOfQuestion = TextBoxTextOfQuestion.Text;
            TextBoxTextOfQuestion.Text = "Enter text of question";
            AbstractQuestion question = new OneAnswerQuestion(textOfQuestion, _crntAnswers, _indexOfRigthAnswer);
            _crntQuestions.Add(question);
            WrapPanelAnswers.Children.Clear();
            _answerCounter = 1;
        }


        private int _answerCounter = 1;
        private void ButtonAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxTextOfAnswer.Text != "" && TextBoxTextOfAnswer.Text != "Enter text of answer")
            {
                Label answer = new Label();
                answer.Content = $"{_answerCounter}. {TextBoxTextOfAnswer.Text}";
                string answerText = TextBoxTextOfAnswer.Text;
                _crntAnswers.Add(answerText);
                TextBoxTextOfAnswer.Text = "Enter text of answer";
                _answerCounter++;
                WrapPanelAnswers.Children.Add(answer);
                TextBoxTextOfAnswer_PreviewMouseDown_Counter = 0;
            }
        }


        private int TextBoxTextOfAnswer_PreviewMouseDown_Counter = 0;
        private void TextBoxTextOfAnswer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBoxTextOfAnswer_PreviewMouseDown_Counter == 0)
            {
                TextBoxTextOfAnswer.Clear();
                TextBoxTextOfAnswer_PreviewMouseDown_Counter += 1;
            }
        }


        private int _textBoxNameOfTest_PreviewMouseDown_Counter = 0;
        private void TextBoxNameOfTest_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_textBoxNameOfTest_PreviewMouseDown_Counter == 0)
            {
                TextBoxNameOfTest.Clear();
                _textBoxNameOfTest_PreviewMouseDown_Counter += 1;
            }
        }


        private int _textBoxTextOfQuestion_PreviewMouseDown_Counter = 0;
        private void TextBoxTextOfQuestion_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_textBoxTextOfQuestion_PreviewMouseDown_Counter == 0)
            {
                TextBoxTextOfQuestion.Clear();
                _textBoxTextOfQuestion_PreviewMouseDown_Counter += 1;
            }
        }


    }
}