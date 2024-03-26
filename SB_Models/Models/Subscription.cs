using System.ComponentModel.DataAnnotations;

namespace SB_Models.Models
{
    public class Subscription
    {
        public string? Query { get; set; }
        public int? Price { get; set; }
        public bool IsValid { get; set; }

        public Subscription() 
        {
            IsValid = false;
        }
    }
}
