using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class Product
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public User Seller { get; set; }
        public byte[] Photo { get; set; }

        public Product(string name, int count, double price, string description, User seller, byte[] photo)
        {
            Name = name;
            Count = count;
            Price = price;
            Description = description;
            Seller = seller;
            Photo = photo;
        }

    }
}
