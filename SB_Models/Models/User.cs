using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SB_Models.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string Name { get; set; }

        public string Subscriptions {  get; set; }

        public MenuState MenuState { get; set; }

        public void AddNewSub(Subscription subscription)
        {
            var listOfSub = JsonConvert.DeserializeObject<List<Subscription>>(Subscriptions) ?? new List<Subscription>();
            listOfSub.Add(subscription);
            Subscriptions = JsonConvert.SerializeObject(listOfSub);
        }

        public void RemoveSub(Subscription subscription) 
        {
            var listOfSub = JsonConvert.DeserializeObject<List<Subscription>>(Subscriptions) ?? new List<Subscription>();
            listOfSub.Remove(subscription);
            Subscriptions = JsonConvert.SerializeObject(listOfSub);
        }

        public void RemoveSub(int index)
        {
            var listOfSub = JsonConvert.DeserializeObject<List<Subscription>>(Subscriptions) ?? new List<Subscription>();
            listOfSub.Remove(listOfSub[index - 1]);
            Subscriptions = JsonConvert.SerializeObject(listOfSub);
        }

        public List<Subscription> GetSub()
        {
            return JsonConvert.DeserializeObject<List<Subscription>>(Subscriptions) ?? new List<Subscription>();
        }

    }
}
