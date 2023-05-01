using Marketplace_design.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MarketplaceKazonberriesExpress.Core.Classes;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        public void GoToBuyerRegistration(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage("Покупатель"));
        }

        public void GoToSellerRegistration(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage("Продавец"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(tbEmail.Text != "" && tbPassword.Password != "")
            {
                var user = Database.FindUserByEmail(tbEmail.Text, tbPassword.Password);
                if(user != null)
                {
                    CurrentUser.User = user;
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля для авторизации!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
