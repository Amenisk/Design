using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class SoldProductInformation
    {
        public ObjectId _id { get; private set; }
        public string Name { get; private set; }
        public string Price { get; private set; }
        public string BuyerFullName { get; private set; }
        public string DateOfPurchase { get; private set; }

        public SoldProductInformation(ObjectId id, string name, string price, string buyerFullName, string dateOfPurchase)
        {
            _id = id;
            Name = name;
            Price = price;
            BuyerFullName = buyerFullName;
            DateOfPurchase = dateOfPurchase;
        }
    }
}
