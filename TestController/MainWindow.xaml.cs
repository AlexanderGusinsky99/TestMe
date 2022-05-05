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
using TelegramManager;
using Telegram.Bot.Types;

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
        private List<Test> _listOfTests = new List<Test>();
        private List<Poll> _listOfPolls = new List<Poll>();
        private List<string> _listOfTestNames;

        public List<string> crntTestAnswers = new List<string>();

        public int _indexOfRigthAnswer = -1;

        private TelegramClient _telegramClient;

        private const string _token = "5214418897:AAGMzUpDI8mf2cVJ0S7kFGa_QheT0LYonMQ";

        List<string> _messageSource;

        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _telegramClient = new TelegramClient(_token, OnMessage);
            _messageSource = new List<string>();
            LBTGCheckChat.ItemsSource = _messageSource;
            ListBoxTListOfTestNames.ItemsSource = _listOfTestNames;
            ListBoxUConnectedUsers.ItemsSource = _telegramClient.ConnectedUsers;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        public List<string> GetTestNames(List<Test> listOfTests)
        {
            List<string> listOfTestNames = new List<string>();
            for (int i = 0; i < listOfTests.Count; i++)
            {
                listOfTestNames.Add(_listOfTests[i].Name);
            }
            return listOfTestNames;
        }

        public void OnMessage(string message)
        {
            _messageSource.Add(message);
        }

        private void ButtonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            string nameOfTest = TextBoxNameOfTest.Text;
            _listOfTests.Add(new Test(nameOfTest, _crntTestQuestions));
            TextBoxNameOfTest.Text = "Enter name of text";
            _crntTestQuestions = new List<AbstractQuestion>();
            WrapPanelAnswers.Children.Clear();
            TextBoxTextOfQuestion.Text = _defaultQuestionText;
            _answerCounter = 1;
            ListBoxTListOfTestNames.ItemsSource = _listOfTestNames;
            ListBoxTListOfTestNames.Items.Refresh();
        }


        private void ButtonTestCreator_Click(object sender, RoutedEventArgs e)
        {
            GridTestCreator.Visibility = Visibility.Visible;
            GridTGCheckChat.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Hidden;
            GridTests.Visibility = Visibility.Hidden;
        }
        private void ButtonPollCreator_Click(object sender, RoutedEventArgs e)
        {
            GridPollCreator.Visibility = Visibility.Visible;
            GridTestCreator.Visibility=Visibility.Hidden;
            GridTGCheckChat.Visibility=Visibility.Hidden;
            GridTests.Visibility = Visibility.Hidden;
        }


        private void ButtonTGCheckChat_Click(object sender, RoutedEventArgs e)
        {
            GridTGCheckChat.Visibility = Visibility.Visible;
            GridTestCreator.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Hidden;
            GridTests.Visibility = Visibility.Hidden;
        }
        private void BottonTTestsAndPolls_Click(object sender, RoutedEventArgs e)
        {
            GridTests.Visibility = Visibility.Visible;
            GridTGCheckChat.Visibility = Visibility.Hidden;
            GridTestCreator.Visibility = Visibility.Hidden;
            GridPollCreator.Visibility = Visibility.Hidden;
            _listOfTestNames = GetTestNames(_listOfTests);
            ListBoxTListOfTestNames.ItemsSource = _listOfTestNames;
        }


        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            
            string textOfQuestion = TextBoxTextOfQuestion.Text;
            TextBoxTextOfQuestion.Text = "Enter text of question";
            AbstractQuestion question = new OneAnswerQuestion(textOfQuestion, crntTestAnswers, _indexOfRigthAnswer);
            _crntTestQuestions.Add(question);
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
                crntTestAnswers.Add(answerText);
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


        private bool _textBoxTextOfQuestion_PreviewMouseDown_Counter = false;
        private void TextBoxTextOfQuestion_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_textBoxTextOfQuestion_PreviewMouseDown_Counter == false)
            {
                TextBoxTextOfQuestion.Clear();
                _textBoxTextOfQuestion_PreviewMouseDown_Counter = true;
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            _telegramClient.Start();
        }

        private void OnTick(object sender, EventArgs e)
        {
            LBTGCheckChat.Items.Refresh();
            ListBoxUConnectedUsers.Items.Refresh();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            _telegramClient.Send(TextBoxTGSender.Text);
        }

        private void ComboBoxTestOrPoll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxTestOrPoll.SelectedIndex)
            {
                case 0:
                    ComboBoxTstPllSelected.ItemsSource = _listOfTestNames;
                    break;
                case 1:

                    break;
                default:
                    break;
            }
        }
    }
}