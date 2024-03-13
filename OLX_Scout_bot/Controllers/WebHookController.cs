using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OLX_Scout_bot.Controllers
{
    [ApiController]
    [Route("/")]
    public class WebHookController : Controller
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<WebHookController> _logger;

        public WebHookController(ITelegramBotClient botClient, ILogger<WebHookController> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        [HttpPost]
        public async void Post(Update update)
        {
            long chatId = update.Message.Chat.Id; //получаем айди чата, куда нам сказать привет
            await _botClient.SendTextMessageAsync(chatId, "Привет!");
        }

        [HttpGet]
        public string Get()
        {
            //Здесь мы пишем, что будет видно если зайти на адрес,
            //указаную в ngrok и launchSettings
            return "Telegram bot was started";
        }
    }
}
