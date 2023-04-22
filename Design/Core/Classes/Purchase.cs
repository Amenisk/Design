using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class Purchase
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public int OrderNumber { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public User Buyer { get; set; }
        public string DateOfPurchase { get; set; }
        public string IssuePointAddress { get; set; }
        public int DeliveryDays { get; set; }
        public double PurchaseCost { get; set; }
        public string Status { get; set; }

        public Purchase(int orderNumber, List<Product> products, User buyer, string issuePointAddress)
        {
            OrderNumber = orderNumber;
            Random rnd = new Random();
            Products = products;
            DateOfPurchase = DateTime.Now.ToString("d");
            Buyer = buyer;
            IssuePointAddress = issuePointAddress;
            DeliveryDays = rnd.Next(1, 6);
            foreach (var p in Products)
            {
                PurchaseCost += p.Price;
            }
            Status = "Оформлен";
        }
    }
}
