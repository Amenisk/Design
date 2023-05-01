using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows.Controls;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfBuyersPage.xaml
    /// </summary>
    public partial class ListOfBuyersPage : Page
    {
        public ListOfBuyersPage()
        {
            InitializeComponent();
            lvAllBuyers.ItemsSource = Database.GetAllBuyers();
        }
    }
}
