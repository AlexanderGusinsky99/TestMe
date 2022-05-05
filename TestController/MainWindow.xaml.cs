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
        public const string _defaultVariant = "Enter text of variant";
        public List<string> _crntTestAnswers = new List<string>();
        public List<string> _crntPollAnswers = new List<string>();
        public List<AbstractQuestion> _crntTestQuestions = new List<AbstractQuestion>();
        public List<AbstractQuestion> _crntPollQuestions = new List<AbstractQuestion>();
        public AbstractQuestion _crntTestQuestion;
        public AbstractQuestion _crntPollQuestion;
        public int _indexOfRigthAnswer = -1;
        private List<Test> _listOfTests = new List<Test>();
        private List<Poll> _listOfPolls = new List<Poll>(); 
        private TelegramClient _telegramClient;
        private const string _token = "5214418897:AAGMzUpDI8mf2cVJ0S7kFGa_QheT0LYonMQ";
        List<string> _messageSourse;
        private DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void ButtonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            string nameOfTest = TextBoxNameOfTest.Text;
            _listOfTests.Add(new Test(nameOfTest, _crntTestQuestions));
            TextBoxNameOfTest.Text = "Enter name of test";
            _crntTestQuestions = new List<AbstractQuestion>();
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
            if (_crntTestQuestion != null && string.IsNullOrEmpty(TextBoxTextOfQuestion.Text) && TextBoxTextOfQuestion.Text != _defaultQuestionText)
            {
                _crntTestQuestion.TextOfQuestion = TextBoxTextOfQuestion.Text;
                _crntTestQuestion.ListOfAnswers = _crntTestAnswers;
                _crntTestQuestions.Add(_crntTestQuestion);

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
                _crntTestAnswers.Add(answerText);
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
            string nameOfPoll = TextBoxNameOfPoll.Text;
            _listOfPolls.Add(new Poll(nameOfPoll, _crntPollQuestions));
            TextBoxNameOfPoll.Text = "Enter name of Poll";
            _crntPollQuestions = new List<AbstractQuestion>();
            WrapPanelVariants.Children.Clear();
            TextBoxVariantsOfPoll.Text = _defaultVariant;
            _variantCounter = 1;
        }

        

        

        private void ComboBoxSelectTypeOfAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            WrapPanelAnswers.Visibility = Visibility.Visible;
            switch (ComboBoxSelectTypeOfAnswer.SelectedIndex)
            {
                case 0:
                    _crntTestQuestion = new OneAnswerQuestion();
                    ButtonAddAnswer.Visibility = Visibility.Visible;
                    WrapPanelAnswers.Visibility = Visibility.Visible;
                    break;
                case 1:
                    _crntTestQuestion = new SeveralAnswersQuestion();
                    ButtonAddAnswer.Visibility = Visibility.Visible;
                    break;
                case 2:
                    _crntTestQuestion = new SortAnswersQuestion();
                    ButtonAddAnswer.Visibility = Visibility.Visible;
                    break;
                case 3:
                    _crntTestQuestion = new OpenAnswerQuestion();
                    WrapPanelAnswers.Visibility = Visibility.Hidden;
                    ButtonAddAnswer.Visibility = Visibility.Hidden;
                    break;
                default:
                    _crntTestQuestion = new OneAnswerQuestion();

                    break;
            }
            _crntTestQuestion.TextOfQuestion = TextBoxTextOfQuestion.Text;
        }


        private int _textBoxNameOfPoll_PreviewMouseDown_Counter = 0;
        private void TextBoxNameOfPoll_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_textBoxNameOfPoll_PreviewMouseDown_Counter == 0)
            {
                TextBoxNameOfPoll.Clear();
                _textBoxNameOfPoll_PreviewMouseDown_Counter += 1;
            }
        }

        
        
           
        private int _textBoxTextOfPoll_PreviewMouseDown_Counter = 0;
        private void TextBoxTextOfPoll_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_textBoxTextOfPoll_PreviewMouseDown_Counter == 0)
            {
                TextBoxTextOfPoll.Clear();
                _textBoxTextOfPoll_PreviewMouseDown_Counter += 1;
            }
        }

        private int TextBoxVariantsOfPoll_PreviewMouseDown_Counter = 0;
        private void TextBoxVariantsOfPoll_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBoxVariantsOfPoll_PreviewMouseDown_Counter == 0)
            {
                TextBoxVariantsOfPoll.Clear();
                TextBoxVariantsOfPoll_PreviewMouseDown_Counter += 1;
            }
        }

        private int _variantCounter = 1;
        private void ButtonAddVariant_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxVariantsOfPoll.Text != "" && TextBoxVariantsOfPoll.Text != _defaultVariant)
            {
                Label variant = new Label();
                variant.Content = $"{_variantCounter}. {TextBoxVariantsOfPoll.Text}";
                string variantText = TextBoxVariantsOfPoll.Text;
                _crntPollAnswers.Add(variantText);
                TextBoxVariantsOfPoll.Text = _defaultVariant;
                _variantCounter++;
                WrapPanelVariants.Children.Add(variant);
                TextBoxVariantsOfPoll_PreviewMouseDown_Counter = 0;
            }


        }
    }
    
}