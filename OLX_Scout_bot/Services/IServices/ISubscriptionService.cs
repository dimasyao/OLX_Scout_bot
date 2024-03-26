using SB_Models.Models;

namespace OLX_Scout_bot.Services.IServices
{
    public interface ISubscriptionService
    {
        public Subscription SplitQuery(string queryAndPrice);
    }
}
