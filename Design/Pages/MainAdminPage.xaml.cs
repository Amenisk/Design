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
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public MainAdminPage()
        {
            InitializeComponent();
            tbCommission.Text = Database.GetMarketplaceCommission().Commission.ToString();
            tbEarnings.Text = Database.GetEarningMarketplace().Balance.ToString();
            tbProductsCount.Text = Database.GetCountAllUnsoldProducts().ToString();
            tbBuyersCount.Text = Database.GetBuyersCount().ToString();
            tbSellersCount.Text = Database.GetSellersCount().ToString();
        }

        private void GoToApplicationList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfSellApplication());
        }

        private void ChangeCommission(object sender, RoutedEventArgs e)
        {
            if(tbCommission != null && int.TryParse(tbCommission.Text, out int value)) 
            { 
                if(value > 0 && value < 100)
                {
                    Database.SetMarketplaceCommission(value);
                }
                else
                {
                    MessageBox.Show("Некорректное число процента!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                tbCommission.Text = "";
                MessageBox.Show("Некорректный формат процента!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoToBuyersList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfBuyersPage());
        }

        private void GoToSellersList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfSellersPage());
        }

        private void ChangePeriod(object sender, SelectionChangedEventArgs e)
        {
            switch (((TextBlock) cmbPeriod.SelectedItem).Text)
            {
                case "День":
                    tbCountSells.Text = Database.GetCountSellsInPeriod(DateTime.Today, DateTime.Today.AddDays(1)).ToString();
                    tbSumCostSells.Text = Database.GetSumCostSellsInPeriod(DateTime.Today, DateTime.Today.AddDays(1)).ToString();
                    break;

                case "Неделя":
                    tbCountSells.Text = Database.GetCountSellsInPeriod(GetStartWeek(), GetEndWeek()).ToString();
                    tbSumCostSells.Text = Database.GetSumCostSellsInPeriod(GetStartWeek(), GetEndWeek()).ToString();
                    break;

                case "Месяц":
                    tbCountSells.Text = Database.GetCountSellsInPeriod(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), 
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1)).ToString();
                    tbSumCostSells.Text = Database.GetSumCostSellsInPeriod(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), 
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1)).ToString();
                    break;

                case "Год":
                    tbCountSells.Text = Database.GetCountSellsInPeriod(new DateTime(DateTime.Now.Year, 1, 1),
                        new DateTime(DateTime.Now.Year + 1, 1, 1)).ToString();
                    tbSumCostSells.Text = Database.GetSumCostSellsInPeriod(new DateTime(DateTime.Now.Year, 1, 1),
                        new DateTime(DateTime.Now.Year + 1, 1, 1)).ToString();
                    break;

                case "Всё время":
                    tbCountSells.Text = Database.GetCountSellsInPeriod(new DateTime(1900, 1, 1),
                        DateTime.Now).ToString();
                    tbSumCostSells.Text = Database.GetSumCostSellsInPeriod(new DateTime(1900, 1, 1),
                        DateTime.Now).ToString();
                    break;
            }
        }

        private DateTime GetStartWeek()
        {
            DateTime date = DateTime.Today;
            while(date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        private DateTime GetEndWeek()
        {
            DateTime date = DateTime.Today;
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(1);
            }

            return date;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            CurrentUser.User = null;
            NavigationService.Navigate(new MainPage());
        }
    }
}
