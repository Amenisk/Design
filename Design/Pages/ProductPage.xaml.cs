using MarketplaceKazonberriesExpress.Core.Classes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private Product _product;
        public ProductPage(ObjectId id)
        {
            InitializeComponent();
            _product = Database.GetProduct(id);
            iPhoto.Source = BytesToImage(_product.Photo);
            tbSellerFullName.Text = _product.Seller.FullName;
            tbName.Text = _product.Name;
            tbCount.Text = _product.Count.ToString();
            tbPrice.Text = _product.Price.ToString();
            tbDescription.Text = _product.Description;
            lvReviews.ItemsSource = Database.GetProductReviewList(id);
        }

        private BitmapImage BytesToImage(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.Basket.Add(_product);
        }

        private void CreateReview(object sender, RoutedEventArgs e)
        {
            if(tbReview.Text != "")
            {
                if(CurrentUser.User != null && CurrentUser.User.Role == "Покупатель")
                {
                    Database.AddReview(new Review(tbReview.Text, _product, CurrentUser.User));
                    NavigationService.Navigate(new ProductPage(_product._id));
                }
                else
                {
                    MessageBox.Show("Отзыв могут оставлять только покупатели!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните отзыв!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
