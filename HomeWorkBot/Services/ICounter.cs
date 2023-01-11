using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HomeWorkBot.Services
{
    public interface ICounter
    {
        Task Count(Message message, string userCountMode, CancellationToken ct);
    }
}
