using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestMe.BLL
{
    public class TelegramClient
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        private LinkedList<long> _chatIDS = new LinkedList<long>();
        public List<string> ConnectedUsers = new List<string>();
        public List<TestMe.BLL.UserMe> ListOfUsers = new List<TestMe.BLL.UserMe>();

        public TelegramClient(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
        }
        public async void Send(string message)
        {
            foreach (var chatID in _chatIDS)
            {
                _client.SendTextMessageAsync(new ChatId(chatID), message);

            }
        }
        public void Start()
        {
            _client.StartReceiving(HandleResive, HandleError);
        }
        private async Task HandleResive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!(_chatIDS.Contains(update.Message.Chat.Id)))
            {
                TestMe.BLL.UserMe user = new TestMe.BLL.UserMe(update.Message.From.FirstName,
                update.Message.From.LastName,
                update.Message.From.Id);
                ListOfUsers.Add(user);
                _chatIDS.AddLast(update.Message.Chat.Id);
                string userName = update.Message.From.FirstName + update.Message.From.LastName;
                ConnectedUsers.Add(userName);
            }
            if (update.Message != null && update.Message.Text != null)
            {
                string crntMessage = update.Message.Text;
                _onMessage(crntMessage);
            }
            else if (update.CallbackQuery != null)
            {
                string crntMessage = update.CallbackQuery.Data;
                _onMessage(crntMessage);
            }
        }
        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
