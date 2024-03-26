using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLX_Scout_bot.Services.IServices;
using SB_DataAccess.Repository.Interface;
using SB_Models.Models;
using SB_Utility;
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
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionService _subscriptionService;

        public WebHookController(ITelegramBotClient botClient, ILogger<WebHookController> logger, IUserRepository userRepository, ISubscriptionService subscriptionService )
        {
            _botClient = botClient;
            _logger = logger;
            _userRepository = userRepository;
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public void Post(Update update)
        {
            var user = _userRepository.FirstOrDefault(x => x.ChatId == update.Message.Chat.Id);

            if (user == null)
            {
                user = new SB_Models.Models.User()
                {
                    ChatId = update.Message.Chat.Id,
                    Name = update.Message.Chat.FirstName + " " + update.Message.Chat.LastName,
                    Subscriptions = ""
                };

                _userRepository.Add(user);
            }

            if (update.Message.Text == "/MainMenu" || update.Message.Text == "/start")
            {
                user.MenuState = MenuState.MainMenu;
                _userRepository.Update(user);
                _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose an option:", replyMarkup: Keyboard.MainMenu());
            }
            else
            if (update.Message.Text == "Add new subscription" || user.MenuState == MenuState.AddNewSubscription)
            {
                if (user.MenuState == MenuState.AddNewSubscription && update.Message.Text == "Back")
                {
                    user.MenuState = MenuState.MainMenu;
                    _userRepository.Update(user);
                    _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose an option:", replyMarkup: Keyboard.MainMenu());
                }
                else if (user.MenuState == MenuState.AddNewSubscription)
                { 
                    var sub = _subscriptionService.SplitQuery(update.Message.Text);

                    if (sub.IsValid)
                    {
                        user.AddNewSub(sub);
                        user.MenuState = MenuState.MainMenu;
                        _userRepository.Update(user);
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Query added successfully!", replyMarkup: Keyboard.MainMenu());
                    }
                    else
                    {
                        user.MenuState = MenuState.MainMenu;
                        _userRepository.Update(user);
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Looks like you didn't follow the input rules!", replyMarkup: Keyboard.MainMenu());
                    }
                }
                else
                {
                    user.MenuState = MenuState.AddNewSubscription;
                    _userRepository.Update(user);
                    _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Write your query in format \"query/price\". \nExample: My query / 100", replyMarkup: Keyboard.AddNewSubMenu());
                }
            }
            else
            if (update.Message.Text == "Delete subscription" || user.MenuState == MenuState.DeleteSubscription)
            {
                if (user.MenuState == MenuState.DeleteSubscription && update.Message.Text == "Back")
                {
                    user.MenuState = MenuState.MainMenu;
                    _userRepository.Update(user);
                    _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose an option:", replyMarkup: Keyboard.MainMenu());
                }
                else if (user.MenuState == MenuState.DeleteSubscription)
                { 
                    var flag = int.TryParse(update.Message.Text, out int numberOfSubscription);

                    if (flag && numberOfSubscription > 0)
                    {
                        user.RemoveSub(numberOfSubscription);
                        user.MenuState = MenuState.MainMenu;
                        _userRepository.Update(user);
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Query deleted successfully!", replyMarkup: Keyboard.MainMenu());
                    }
                    else
                    {
                        user.MenuState = MenuState.MainMenu;
                        _userRepository.Update(user);
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Looks like you didn't have such subscription!", replyMarkup: Keyboard.MainMenu());
                    }
                }
                else
                {
                    user.MenuState = MenuState.DeleteSubscription;
                    _userRepository.Update(user);
                    _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose subscription what you want to delete.", replyMarkup: Keyboard.ShowSubscriptionsButtons(user.GetSub()));
                }
            }
            else
            if (update.Message.Text == "My subscription's" || user.MenuState == MenuState.MySub)
            {
                if (user.MenuState == MenuState.MySub && update.Message.Text == "Back")
                {
                    user.MenuState = MenuState.MainMenu;
                    _userRepository.Update(user);
                    _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose an option:", replyMarkup: Keyboard.MainMenu());
                }
                else
                {
                    var subs = user.GetSub();

                    if (subs.Count != 0)
                    {
                        var text = "";
                        var i = 1;
                        foreach (var sub in user.GetSub())
                        {
                            text += i + ". " + sub.Query + " " + sub.Price + "\n";
                            i++;
                        }
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, text);
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose an option:", replyMarkup: Keyboard.MySubMenu());
                        user.MenuState = MenuState.MySub;
                    }
                    else
                    {
                        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "You haven't subscriptions yet. \nChoose an option:", replyMarkup: Keyboard.MainMenu());
                        user.MenuState = MenuState.MainMenu;
                    }
                    
                    _userRepository.Update(user);
                }
            }

            _userRepository.Save();
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
