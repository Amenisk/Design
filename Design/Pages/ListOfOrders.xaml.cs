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
