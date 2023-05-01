using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows.Controls;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfSellersPage.xaml
    /// </summary>
    public partial class ListOfSellersPage : Page
    {
        public ListOfSellersPage()
        {
            InitializeComponent();
            lvAllSellers.ItemsSource = Database.GetAllSellers();
        }
    }
}
