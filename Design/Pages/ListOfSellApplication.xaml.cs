using MarketplaceKazonberriesExpress.Core.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfSellApplication.xaml
    /// </summary>
    public partial class ListOfSellApplication : Page
    {
        public ListOfSellApplication()
        {
            InitializeComponent();
            lvProductApplications.ItemsSource = Database.GetSellAplicationsList();
        }

        private void Approve(object sender, RoutedEventArgs e)
        {
            if(lvProductApplications.SelectedItem != null) 
            {
                Database.AddProduct((Product)lvProductApplications.SelectedItem);
                Database.RemoveProductApplication((Product)lvProductApplications.SelectedItem);
                lvProductApplications.ItemsSource = Database.GetSellAplicationsList();
            }        
        }

        private void Reject(object sender, RoutedEventArgs e)
        {
            if (lvProductApplications.SelectedItem != null)
            {
                Database.AddProduct((Product)lvProductApplications.SelectedItem);
                Database.RemoveProductApplication((Product)lvProductApplications.SelectedItem);
                lvProductApplications.ItemsSource = Database.GetSellAplicationsList();
            }
        }
    }
}
