using MarketplaceKazonberriesExpress.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для MakeOrderPage.xaml
    /// </summary>
    public partial class MakeOrderPage : Page
    {
        public MakeOrderPage()
        {
            InitializeComponent();
            lvBasket.ItemsSource = CurrentUser.Basket;
            CountCostOrder();
        }

        private void RemoveFromBusket(object sender, RoutedEventArgs e)
        {
            CurrentUser.Basket.Remove((Product) lvBasket.SelectedItem);
            NavigationService.Navigate(new MakeOrderPage());
            CountCostOrder();
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            if(CurrentUser.User != null && CurrentUser.User.Role == "Покупатель")
            {
                if(CurrentUser.Basket.Count != 0)
                {
                    if (cbIssuePoints.SelectedItem != null)
                    {
                        MakePurchase();
                        var purchase = new Purchase(Database.GetLastNumberOfPurchase() + 1, CurrentUser.Basket, CurrentUser.User, ((TextBlock)cbIssuePoints.SelectedItem).Text.ToString());
                        Database.AddPurchase(purchase);
                        CurrentUser.Basket.Clear();
                        MessageBox.Show("Покупка успешно оформлена!");
                        NavigationService.Navigate(new MainPage());

                    }
                    else
                    {
                        MessageBox.Show("Сначала выберите точку доставки!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Сначала заполните корзину!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
            else
            {
                MessageBox.Show("Чтобы оформить заказ, нужно авторизироваться как покупатель!", "Ошибка!", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void MakePurchase()
        {
           
            foreach (var p in CurrentUser.Basket)
            {

                if(!Database.CheckCountProduct(p._id))
                {
                    MessageBox.Show($"Товар {p.Name} закончился! Уберите его из заказа!", "Ошибка!",
                MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            foreach(var p in CurrentUser.Basket)
            {
                Database.ChangeCountProduct(p._id);
            }
            
        }

        private void CountCostOrder()
        {
            tbCost.Text = CurrentUser.CountBasketCost().ToString();
        }

        private void GoToProductPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(((Product)lvBasket.SelectedItem)._id));
        }
    }
}
