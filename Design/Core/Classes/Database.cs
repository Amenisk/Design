using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public static class Database
    {
        public static void SaveUserToDB(User user)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            collection.InsertOne(user);
        }

        public static User FindUserByEmail(string email, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            return collection.Find(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault();
        }

        public static void ChangeInformationOfUser(User user)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            collection.ReplaceOne(x => x.Email == user.Email, user);
        }

        public static bool CheckEmail(string email)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");
            var user = collection.Find(x => x.Email.Equals(email)).FirstOrDefault();

            return user != null;
        }

        public static List<User> GetAllSellers()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            return collection.Find(x => x.Role == "Продавец").ToList();
        }

        public static List<User> GetAllBuyers()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            return collection.Find(x => x.Role == "Покупатель").ToList();
        }

        public static void AddProductApplication(Product product)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("ProductApplications");

            collection.InsertOne(product);

        }

        public static List<Product> GetSellAplicationsList()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("ProductApplications");

            return collection.Find(x => x.Name != null).ToList();
        }

        public static void RemoveProductApplication(Product product)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("ProductApplications");

            collection.DeleteOne(x => x._id == product._id);
        }

        public static void AddProduct(Product product)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");

            collection.InsertOne(product);
        }

        public static int GetCountAllUnsoldProducts()
        {

            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");

            int count = 0;

            foreach (var product in collection.Find(x => x._id != null).ToList())
            {
                count += product.Count;
            }

            return count;
        }

        public static List<Product> GetProductsList()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");

            return collection.Find(x => x.Name != null).ToList();
        }

        public static int GetLastNumberOfPurchase()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            var purchase = collection.Find(x => x.OrderNumber != 0).Sort("{OrderNumber: -1}").FirstOrDefault<Purchase>();

            if (purchase is not null)
            {
                return purchase.OrderNumber;
            }
            else
            {
                return 0;
            }
        }

        public static void AddPurchase(Purchase purchase)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            CalculationsOfSale(purchase);
            collection.InsertOne(purchase);
        }

        public static bool ChangeCountProduct(ObjectId idProduct)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");
            var p = collection.Find(x => x._id == idProduct).FirstOrDefault();

            if (p.Count == 0)
            {
                return false;
            }

            var filter = Builders<Product>.Filter.Eq("_id", idProduct);
            var update = Builders<Product>.Update.Set("Count", --p.Count);
            collection.UpdateOne(filter, update);
            return true;
        }

        public static bool CheckCountProduct(ObjectId idProduct)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");
            var p = collection.Find(x => x._id == idProduct).FirstOrDefault();

            if (p.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static List<Purchase> GetPurchacesListByBuyer(User buyer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            return collection.Find(x => x.Buyer._id == buyer._id).ToList();
        }

        public static Purchase GetPurchaseByOrderNumber(int orderNumber)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            return collection.Find(x => x.OrderNumber == orderNumber).FirstOrDefault();
        }

        public static int GetSoldProductsCount(ObjectId sellerId)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            int count = 0;

            var purchaseList = collection.Find(x => x._id != null).ToList();

            if (purchaseList != null)
            {
                foreach (var p in purchaseList)
                {
                    foreach (var product in p.Products)
                    {
                        if (product.Seller._id == sellerId)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public static int GetUnsoldProductsCount(ObjectId sellerId)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");

            int count = 0;

            var productsList = collection.Find(x => x.Seller._id == sellerId).ToList();

            foreach (var product in productsList)
            {
                count += product.Count;
            }

            return count;
        }

        public static int GetBuyersCount()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            int count = 0;

            var buyersList = collection.Find(x => x.Role == "Покупатель").ToList();

            return buyersList.Count;
        }

        public static int GetSellersCount()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<User>("Users");

            int count = 0;

            var sellersList = collection.Find(x => x.Role == "Продавец").ToList();

            return sellersList.Count;
        }

        public static List<Product> GetUnsoldProductList(ObjectId sellerId)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");

            return collection.Find(x => x.Seller._id == sellerId).ToList();
        }

        public static List<SoldProductInformation> GetSoldProductInformationList(ObjectId sellerId)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            var purchaseList = collection.Find(x => x._id != null).ToList();
            var productInformationList = new List<SoldProductInformation>();

            if (purchaseList != null)
            {
                foreach (var p in purchaseList)
                {
                    foreach (var product in p.Products)
                    {
                        if (product.Seller._id == sellerId)
                        {
                            productInformationList.Add(new SoldProductInformation(product._id, product.Name, product.Price.ToString(), p.Buyer.FullName, p.DateOfPurchase));
                        }
                    }
                }
            }

            return productInformationList;
        }

        public static Product GetProduct(ObjectId id)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Product>("Products");

            return collection.Find(x => x._id == id).FirstOrDefault();
        }

        public static void AddReview(Review review)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Review>("Reviews");

            collection.InsertOne(review);
        }

        public static List<Review> GetProductReviewList(ObjectId productId)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Review>("Reviews");

            return collection.Find(x => x.ProductReview._id == productId).ToList();
        }

        public static void SetMarketplaceCommission(int commission)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<MarketplaceCommission>("MarketplaceCommission");

            var com = GetMarketplaceCommission();

            if (com != null)
            {
                var filter = Builders<MarketplaceCommission>.Filter.Eq("_id", com._id);
                var update = Builders<MarketplaceCommission>.Update.Set("Commission", commission);
                collection.UpdateOne(filter, update);
            }
            else
            {
                collection.InsertOne(new MarketplaceCommission(commission));
            }
        }

        public static MarketplaceCommission GetMarketplaceCommission()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<MarketplaceCommission>("MarketplaceCommission");

            var com = collection.Find(x => x._id != null).FirstOrDefault();

            if (com != null)
            {
                return com;
            }
            else
            {
                return new MarketplaceCommission(0);
            }
        }

        public static void CalculationsOfSale(Purchase purchase)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Earning>("Earnings");

            var filterM = Builders<Earning>.Filter.Where(x => x.OwnerId == null);

            foreach (var product in purchase.Products)
            {
                var earningMarketplace = GetEarningMarketplace();
                var earningSeller = GetEarningSeller(product.Seller._id);
                var com = GetMarketplaceCommission();
                var rightPrice = product.Price * ((100.0 - com.Commission) / 100.0);

                var filter = Builders<Earning>.Filter.Eq("OwnerId", earningSeller.OwnerId);
                var update = Builders<Earning>.Update.Set("Balance", Math.Round(earningSeller.Balance + rightPrice, 2));
                collection.UpdateOne(filter, update);

                var updateM = Builders<Earning>.Update.Set("Balance", Math.Round(earningMarketplace.Balance + product.Price - rightPrice, 2));
                collection.UpdateOne(filterM, updateM);
            }
        }

        public static Earning GetEarningSeller(ObjectId id)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Earning>("Earnings");

            var earningSeller = collection.Find(x => x.OwnerId == id).FirstOrDefault();

            if (earningSeller == null)
            {
                collection.InsertOne(new Earning(id));
            }

            return collection.Find(x => x.OwnerId == id).FirstOrDefault();
        }

        public static Earning GetEarningMarketplace()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Earning>("Earnings");

            var earningMarketPlace = collection.Find(x => x.OwnerId == null).FirstOrDefault();

            if (earningMarketPlace == null)
            {
                collection.InsertOne(new Earning());
            }

            return collection.Find(x => x.OwnerId == null).FirstOrDefault();
        }

        public static int GetCountSellsInPeriod(DateTime startDate, DateTime endDate)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");

            var purchaseList = collection.Find(x => DateTime.Parse(x.DateOfPurchase) >= startDate
                && DateTime.Parse(x.DateOfPurchase) < endDate).ToList();
            int count = 0;

            foreach (var purchase in purchaseList)
            {
                count += purchase.Products.Count;
            }

            return count;
        }

        public static double GetSumCostSellsInPeriod(DateTime startDate, DateTime endDate)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");
            var purchaseList = collection.Find(x => DateTime.Parse(x.DateOfPurchase) >= startDate
                && DateTime.Parse(x.DateOfPurchase) < endDate).ToList();
            double sum = 0;

            foreach (var purchase in purchaseList)
            {
                sum += purchase.PurchaseCost;
            }

            return sum;
        }

        public static void UpdatePurchaseStatus()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Purchase>("Purchaces");
            var update = Builders<Purchase>.Update.Set("Status", "Доставлен");

            foreach (var purchase in collection.Find(x => x._id != null).ToList())
            {
                if (purchase.Status != "Доставлен" && DateTime.Parse(purchase.DateOfPurchase).
                    AddDays(purchase.DeliveryDays) < DateTime.Now)
                {
                    var filter = Builders<Purchase>.Filter.Eq("_id", purchase._id);
                    collection.UpdateOne(filter, update);
                }
            }
        }

        public static void SaveBasket(Basket basket)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Basket>("Basket");

            if (collection.Count(x => x._id != null) != 0)
            {
                var filter = Builders<Basket>.Filter.Where(x => x._id != null);
                var update = Builders<Basket>.Update.Set("Products", basket.Products);

                collection.UpdateOne(filter, update);
            }
            else
            {
                collection.InsertOne(basket);
            }
        }

        public static Basket GetSavedBasket()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Marketplace");
            var collection = database.GetCollection<Basket>("Basket");

            return collection.Find(x => x._id != null).FirstOrDefault();
        }
    }
}
