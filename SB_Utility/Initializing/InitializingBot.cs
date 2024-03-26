using System;
using System.Diagnostics;
using Telegram.Bot;

namespace SB_Utility.Initializing
{
    public class InitializingBot : IInitializingBot
    {
        private readonly string url = "https://destined-guinea-charmed.ngrok-free.app";
        private readonly ITelegramBotClient _botClient;

        public InitializingBot(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public void Start()
        {
            _botClient.SetWebhookAsync(url);
        }
    }
}