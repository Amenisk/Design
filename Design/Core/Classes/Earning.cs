using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Buffers;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class Earning
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        public double Balance { get; set; }
        public ObjectId? OwnerId { get; set; }

        public Earning(ObjectId ownerId)
        {
            Balance = 0;
            OwnerId = ownerId;
        }

        public Earning()
        {
            Balance = 0;
        }
    }
}
