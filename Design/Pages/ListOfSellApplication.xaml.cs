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
