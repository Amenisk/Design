using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalesStatisticPage.xaml
    /// </summary>
    public partial class SalesStatisticPage : Page
    {
        public SalesStatisticPage()
        {
            InitializeComponent();
            tbSaleSum.Text = Database.GetEarningSeller(CurrentUser.User._id).Balance.ToString();
            tbSoldProductsCount.Text = Database.GetSoldProductsCount(CurrentUser.User._id).ToString();
            tbUnsoldProductList.Text = Database.GetUnsoldProductsCount(CurrentUser.User._id).ToString();
        }

        private void GoToUnsoldProductList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductListPage());
        }

        private void GoToSoldProducts(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SoldProductsList());
        }
    }
}
