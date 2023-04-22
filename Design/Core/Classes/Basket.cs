using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class Basket
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        public List<Product> Products { get; set; } = new List<Product>();

        public Basket(List<Product> products)
        {
            Products = products;
        }

    }
}
