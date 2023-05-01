using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для SoldProductsList.xaml
    /// </summary>
    public partial class SoldProductsList : Page
    {
        public SoldProductsList()
        {
            InitializeComponent();
            lvSoldProducts.ItemsSource = Database.GetSoldProductInformationList(CurrentUser.User._id);
        }

        private void GoToProductPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(((SoldProductInformation) lvSoldProducts.SelectedItem)._id));
        }
    }
}
