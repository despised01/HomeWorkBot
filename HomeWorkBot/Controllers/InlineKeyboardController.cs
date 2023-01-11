using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using HomeWorkBot.Services;

namespace HomeWorkBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).CountMode = callbackQuery.Data;

            string countMode = callbackQuery.Data switch
            {
                "Symbols" => $"Подсчёт количества символов{Environment.NewLine}{Environment.NewLine}Введите текст",
                "Numbers" => $"Вычисление суммы чисел{Environment.NewLine}{Environment.NewLine}Введите числа (через пробел)",
                _ => String.Empty
            };

            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Режим</b>" + $" - {countMode} {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
