using MarketplaceKazonberriesExpress.Core.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<Product> _productList = new List<Product>();
        public MainPage()
        {
            InitializeComponent();
            _productList = Database.GetProductsList();
            lvProducts.ItemsSource = _productList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToBusket(object sender, RoutedEventArgs e)
        {
            if (lvProducts.SelectedItem != null)
            {
                CurrentUser.Basket.Add((Product)lvProducts.SelectedItem);
            }
            else
            {
                MessageBox.Show("Сначала выберите товар!", "Ошибка!",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void GoToProductPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(((Product)lvProducts.SelectedItem)._id));
        }

        private void FindByName(object sender, RoutedEventArgs e)
        {
            var list = new List<Product>();
            _productList = Database.GetProductsList();
            UseFilter(sender, e);

            if (tbFind.Text != "")
            {
                foreach (var product in _productList)
                {
                    if (product.Name.ToLower().Contains(tbFind.Text.ToLower()))
                    {
                        list.Add(product);
                    }
                }

                _productList = list;
                lvProducts.ItemsSource = _productList;
            }
            else
            {
                lvProducts.ItemsSource = Database.GetProductsList();
            }
        }

        private void UseFilter(object sender, RoutedEventArgs e)
        {
            double valueFrom = 0;
            double valueBefore = 0;

            if (cmbOrderBy.SelectedItem != null)
            {
                _productList = _productList.OrderBy(x => x.Price).ToList();
                if (((ComboBoxItem) cmbOrderBy.SelectedItem).Content.ToString() == "Возрастанию цены")
                {
                    lvProducts.ItemsSource = _productList;
                }
                else
                {
                    _productList.Reverse();
                    lvProducts.ItemsSource = _productList;
                }

                if (tbFrom.Text == "" && tbBefore.Text == "")
                {
                    return;
                }
            }

            if (double.TryParse(tbFrom.Text, out valueFrom) | double.TryParse(tbBefore.Text, out valueBefore))
            {
                SortFromBefore(valueFrom, valueBefore);
            }
        }

        private void SortFromBefore(double valueFrom, double valueBefore)
        {
            if (valueBefore < 0 || valueFrom < 0)
            {
                MessageBox.Show("Значение фильтра не может быть меньше 0!", "Ошибка!",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (valueBefore == 0)
            {
                valueBefore = double.MaxValue;
            }

            if (valueFrom > valueBefore)
            {
                MessageBox.Show("Первый параметр фильтра должен быть меньше второго", "Ошибка!",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var list = new List<Product>();

            foreach (var product in _productList)
            {
                if (product.Price >= valueFrom && product.Price <= valueBefore)
                {
                    list.Add(product);
                }
            }

            _productList = list;
            lvProducts.ItemsSource = _productList;
        }
        private void RemoveFilter(object sender, RoutedEventArgs e)
        {
            _productList = Database.GetProductsList();
            lvProducts.ItemsSource = _productList;
            tbBefore.Text = "";
            tbFind.Text = "";
            tbFrom.Text = "";
            cmbOrderBy.SelectedItem = null;
        }
    }
}
