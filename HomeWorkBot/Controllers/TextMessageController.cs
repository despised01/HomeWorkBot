using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using HomeWorkBot.Services;

namespace HomeWorkBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;
        private readonly ICounter _counter;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, ICounter counter)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _counter = counter;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    _memoryStorage.GetSession(message.Chat.Id).CountMode = null;

                    var buttons = new List<InlineKeyboardButton[]>();

                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Символы", $"Symbols"),
                        InlineKeyboardButton.WithCallbackData($"Числа" , $"Numbers")
                    });

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, 
                        $"\ud83e\uddd1\u200d\ud83c\udfeb<b> Выберите необходимый режим</b> { Environment.NewLine}",
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    break;
            }

            string userCountMode = _memoryStorage.GetSession(message.Chat.Id).CountMode;

            await _counter.Count(message, userCountMode, ct);
        }
    }
}