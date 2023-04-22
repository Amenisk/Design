using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class Review
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public string ReviewText { get; set; }
        public Product ProductReview { get; set; }
        public User Reviewer { get; set; }

        public Review(string reviewText, Product productReview, User reviewer)
        {
            ReviewText = reviewText;
            ProductReview = productReview;
            Reviewer = reviewer;
        }
    }
}
