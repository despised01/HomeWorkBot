using System;
using System.Text;
using HomeWorkBot.Models;

namespace HomeWorkBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}