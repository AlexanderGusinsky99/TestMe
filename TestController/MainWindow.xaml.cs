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
        public const string _defaultQuestionText = "Enter text of question";
        public const string _defaultAnswerText = "Enter text of answer";
        public List<string> _crntAnswers = new List<string>();
        public List<AbstractQuestion> _crntQuestions = new List<AbstractQuestion>();
        public AbstractQuestion _crntQuestion;
        public int _indexOfRigthAnswer = -1;
        private List<Test> _listOfTests = new List<Test>();
        //private List<Poll> _listOfPolls = new List<Poll>(); Добавить лист Опросников
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
            TextBoxNameOfTest.Text = "Enter name of test";
            _crntQuestions = new List<AbstractQuestion>();
            WrapPanelAnswers.Children.Clear();
            TextBoxTextOfQuestion.Text = _defaultQuestionText;
            _answerCounter = 1;

        }
        private void ButtonTestCreator_Click(object sender, RoutedEventArgs e)
        {
            GridTests.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Hidden;
            GridTestCreator.Visibility = Visibility.Visible;

        }
        private void ButtonTGCheckChat_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Hidden;
            GridTests.Visibility = Visibility.Hidden;
        }


        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (_crntQuestion != null && string.IsNullOrEmpty(TextBoxTextOfQuestion.Text) && TextBoxTextOfQuestion.Text != _defaultQuestionText)
            {
                _crntQuestion.TextOfQuestion = TextBoxTextOfQuestion.Text;
                _crntQuestion.ListOfAnswers = _crntAnswers;
                _crntQuestions.Add(_crntQuestion);

            }

            TextBoxTextOfQuestion.Text = _defaultQuestionText;
            TextBoxTextOfAnswer.Text = _defaultAnswerText;
            WrapPanelAnswers.Children.Clear();
            ButtonAddAnswer.Visibility = Visibility.Visible;
            WrapPanelAnswers.Visibility = Visibility.Visible;



        }


        private int _answerCounter = 1;
        private void ButtonAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxTextOfAnswer.Text != "" && TextBoxTextOfAnswer.Text != _defaultAnswerText)
            {
                Label answer = new Label();
                answer.Content = $"{_answerCounter}. {TextBoxTextOfAnswer.Text}";
                string answerText = TextBoxTextOfAnswer.Text;
                _crntAnswers.Add(answerText);
                TextBoxTextOfAnswer.Text = _defaultAnswerText;
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

      

        private void ButtonPollCreator_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Hidden;
            GridTests.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Visible;
        }

        private void ButtonTestsAndPolls_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Hidden;
            GridTests.Visibility = Visibility.Visible;
        }

        private void ButtonCreatePoll_Click(object sender, RoutedEventArgs e)
        {

        }

        

        

        private void ComboBoxSelectTypeOfAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            WrapPanelAnswers.Visibility = Visibility.Visible;
            switch (ComboBoxSelectTypeOfAnswer.SelectedIndex)
            {
                case 0:
                    _crntQuestion = new OneAnswerQuestion();
                    ButtonAddAnswer.Visibility = Visibility.Visible;
                    WrapPanelAnswers.Visibility = Visibility.Visible;
                    break;
                case 1:
                    _crntQuestion = new SeveralAnswersQuestion();
                    ButtonAddAnswer.Visibility = Visibility.Visible;
                    break;
                case 2:
                    _crntQuestion = new SortAnswersQuestion();
                    ButtonAddAnswer.Visibility = Visibility.Visible;
                    break;
                case 3:
                    _crntQuestion = new OpenAnswerQuestion();
                    WrapPanelAnswers.Visibility = Visibility.Hidden;
                    ButtonAddAnswer.Visibility = Visibility.Hidden;
                    break;
                default:
                    _crntQuestion = new OneAnswerQuestion();

                    break;
            }
            _crntQuestion.TextOfQuestion = TextBoxTextOfQuestion.Text;
        }
    }
}