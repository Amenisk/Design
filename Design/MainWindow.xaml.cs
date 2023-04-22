using Design.Pages;
using MarketplaceKazonberriesExpress.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.UpdatePurchaseStatus();
            MainFrame.Navigate(new MainPage());
            CurrentUser.Basket = Database.GetSavedBasket().Products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentUser.User == null)
            {
                MainFrame.Navigate(new AuthorizationPage());
            }
            else
            {
                if (CurrentUser.User.Role == "Покупатель")
                {
                    MainFrame.Navigate(new BuyerAccountPage());
                } 
                else if (CurrentUser.User.Role == "Продавец")
                {
                    MainFrame.Navigate(new SellerAccountPage());
                }
                else
                {
                    MainFrame.Navigate(new MainAdminPage());
                }
                   
            }
            
        }

        private void GoToBasket(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MakeOrderPage());
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if(MainFrame.NavigationService.CanGoBack)
            {
                switch ((MainFrame.Content as Page).Title)
                {
                    case "ListOfBuyersPage":
                        MainFrame.Navigate(new MainAdminPage());
                        break;

                    case "ListOfOrders":
                        MainFrame.Navigate(new BuyerAccountPage()); 
                        break;

                    case "ListOfSellApplication":
                        MainFrame.Navigate(new MainAdminPage());
                        break;

                    case "ListOfSellersPage":
                        MainFrame.Navigate(new MainAdminPage());
                        break;

                    case "OrderPage":
                        MainFrame.Navigate(new ListOfOrders());
                        break;

                    case "ProductListPage":
                        MainFrame.Navigate(new SalesStatisticPage());
                        break;

                    case "ProductSellApllicationPage":
                        MainFrame.Navigate(new SellerAccountPage());
                        break;

                    case "RegistrationPage":
                        MainFrame.Navigate(new AuthorizationPage());
                        break;

                    case "SalesStatisticPage":
                        MainFrame.Navigate(new SellerAccountPage());
                        break;

                    case "SoldProductsList":
                        MainFrame.Navigate(new SalesStatisticPage());
                        break;

                    default:
                        MainFrame.Navigate(new MainPage());
                        break;
                }
            } 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Database.SaveBasket(new Basket(CurrentUser.Basket));
        }
    }
}
