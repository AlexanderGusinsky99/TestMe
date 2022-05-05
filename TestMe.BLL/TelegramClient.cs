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
        private List<long> _userIDs;
        public TelegramClient(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            _userIDs = new List<long>();
        }
        public void Start()
        {
            _client.StartReceiving(HandleResive, HandleError);
        }
        private async Task HandleResive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null && update.Message.Text != null) 
            {
                if (!_userIDs.Contains(update.Message.Chat.Id))
                {
                    _userIDs.Add(update.Message.Chat.Id);
                }
            }
            string userNameAndTextMessage = $"{update.Message.Chat.Id} {update.Message.Chat.FirstName} {update.Message.Chat.LastName}" +
                $"{update.Message.Text}";
            _onMessage(userNameAndTextMessage);
        }
        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        { 
            return Task.CompletedTask;
        }
    }
}
