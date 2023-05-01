using MarketplaceKazonberriesExpress.Core.Classes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductSellApllicationPage.xaml
    /// </summary>
    public partial class ProductSellApllicationPage : Page
    {
        private byte[] _photo;
        public ProductSellApllicationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var BtnSelect = sender as Button;
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    _photo = File.ReadAllBytes(dialog.FileName);
                }
            }
            catch
            {
                MessageBox.Show("Только фотографии!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(tbName.Text != "" && tbPrice.Text != "" && tbCount.Text != "" 
                && tbDescription.Text != "" && _photo != null)
            {
                if(!int.TryParse(tbCount.Text, out var count)) 
                {
                    MessageBox.Show("Некорректный формат количества!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(tbPrice.Text, out var price))
                {
                    MessageBox.Show("Некорректный формат цены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Database.AddProductApplication(new Product(tbName.Text, count, 
                    Math.Round(price, 2), tbDescription.Text, CurrentUser.User, _photo));
                NavigationService.Navigate(new SellerAccountPage());
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
