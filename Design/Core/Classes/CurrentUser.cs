using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public static class CurrentUser
    {
        public static User? User { get; set; }

        public static List<Product> Basket { get; set; } = new List<Product>();

        public static double CountBasketCost()
        {
            double cost = 0;

            foreach (var p in Basket)
            {
                cost += p.Price;
            }

            return cost;
        }
    }
}
