using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private Purchase _purchase;
        public OrderPage(int orderNumber)
        {
            InitializeComponent();
            _purchase = Database.GetPurchaseByOrderNumber(orderNumber);
            lvProducts.Items.Clear();
            lvProducts.ItemsSource = _purchase.Products;
            tbOrderNumber.Text = _purchase.OrderNumber.ToString();

            double cost = 0;
            foreach(var p in _purchase.Products)
            {
                cost += p.Price;
            }
            tbCost.Text = cost.ToString();

            tbDateOfPurchase.Text = _purchase.DateOfPurchase;
            tbStatus.Text = _purchase.Status;
        }

        private void GoToProductPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(((Product) lvProducts.SelectedItem)._id));
        }
    }
}
