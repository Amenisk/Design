using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public ProductListPage()
        {
            InitializeComponent();
            lvProducts.ItemsSource = Database.GetUnsoldProductList(CurrentUser.User._id);       
        }

        private void GoToProductPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(((Product) lvProducts.SelectedItem)._id));
        }
    }
}
