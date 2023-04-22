using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class MarketplaceCommission
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public int Commission { get; set; }

        public MarketplaceCommission(int commission)
        {
            Commission = commission;
        }
    }
}
