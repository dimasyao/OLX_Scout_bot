using SB_Models.Models;
using System.Security.Cryptography.Xml;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace SB_Utility
{
    public static class Keyboard
    {
        public static ReplyKeyboardMarkup MainMenu()
        {
            var button1 = new KeyboardButton("Add new subscription");
            var button2 = new KeyboardButton("My subscription's");
            return new ReplyKeyboardMarkup(new[] { new[] { button1, button2 } }) { ResizeKeyboard = true };
        }

        public static ReplyKeyboardMarkup MySubMenu()
        {
            var button1 = new KeyboardButton("Back");
            var button2 = new KeyboardButton("Delete subscription");
            return new ReplyKeyboardMarkup(new[] { new[] { button1, button2 } }) { ResizeKeyboard = true };
        }

        public static ReplyKeyboardMarkup AddNewSubMenu()
        {
            var button = new KeyboardButton("Back");
            return new ReplyKeyboardMarkup(new[] { new[] { button }}) { ResizeKeyboard = true };
        }

        public static ReplyKeyboardMarkup ShowSubscriptionsButtons(List<Subscription> subscriptions)
        {
            var buttons = new List<KeyboardButton>() { new KeyboardButton("Back")};

            var i = 1;

            foreach (var subscription in subscriptions) 
            {
                buttons.Add(new KeyboardButton(i.ToString()));
                i++;
            }

            return new ReplyKeyboardMarkup(buttons) {ResizeKeyboard = true};
        }
    }
}
