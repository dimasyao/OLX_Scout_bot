using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using OLX_Scout_bot.Controllers;
using OLX_Scout_bot.Services.IServices;
using SB_DataAccess.Repository.Interface;
using SB_Models.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OLX_Scout_bot.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public SubscriptionService() 
        {
        }

        public Subscription SplitQuery(string queryAndPrice)
        {
            var parts = queryAndPrice.Split('/');

            if (parts.Length != 2)
            {
                return new Subscription();
            }

            var query = parts[0].Trim();
            int price;

            string priceStr = parts[1].Trim();

            if (int.TryParse(priceStr, out int number))
            {
                price = number;
            }
            else
            {
                return new Subscription();
            }

            return new Subscription() { Query = query, Price = price, IsValid = true};

        }
    }
}
