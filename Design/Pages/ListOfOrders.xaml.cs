using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfOrders.xaml
    /// </summary>
    public partial class ListOfOrders : Page
    {
        public ListOfOrders()
        {
            InitializeComponent();
            lvOrders.ItemsSource = Database.GetPurchacesListByBuyer(CurrentUser.User);
        }

        private void GoToOrderPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new OrderPage(((Purchase) lvOrders.SelectedItem).OrderNumber));
        }
    }
}
