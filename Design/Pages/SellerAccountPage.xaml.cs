using MarketplaceKazonberriesExpress.Core.Classes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Design.Pages
{
    /// <summary>
    /// Логика взаимодействия для SellerAccountPage.xaml
    /// </summary>
    public partial class SellerAccountPage : Page
    {
        public SellerAccountPage()
        {
            InitializeComponent();
            tbEmail.Text = CurrentUser.User.Email;
            tbFullName.Text = CurrentUser.User.FullName;
            tbPhoneNumber.Text = CurrentUser.User.PhoneNumber;
            tbITN.Text = CurrentUser.User.ITN;
            tbPassportData.Text = CurrentUser.User.PassportData;
            tbPassword.Text = "";

            for (int i = 0; i < CurrentUser.User.Password.Length; i++)
            {
                tbPassword.Text += '*';
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
            CurrentUser.User = null;
        }

        private void ChangeData(object sender, RoutedEventArgs e)
        {
            tbFullName.IsEnabled = true;
            tbPhoneNumber.IsEnabled = true;
            tbPassword.IsEnabled = true;
            tbPassword.IsEnabled = true;
            tbITN.IsEnabled = true;
            tbPassportData.IsEnabled = true;
            tbPassword.Text = CurrentUser.User.Password;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "" && tbPhoneNumber.Text != "" && tbFullName.Text != "" 
                && tbPassword.Text != "" && tbPassportData.Text != "" && tbITN.Text != "")
            {
                if (!CheckPnoneNumber(tbPhoneNumber.Text))
                {
                    MessageBox.Show("Некорректный номер телефона!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!CheckITN(tbITN.Text))
                {
                    MessageBox.Show("Некорректный ИНН!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!CheckPasportData(tbPassportData.Text))
                {
                    MessageBox.Show("Некорректные паспортные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (tbEmail.Text.Length > 30)
                {
                    MessageBox.Show("Длина Email не должна быть больше 30 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (tbFullName.Text.Length > 30)
                {
                    MessageBox.Show("Длина имени не должна быть больше 30 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (tbFullName.Text.Length > 20)
                {
                    MessageBox.Show("Длина пароля не должна быть больше 20 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = new User(tbFullName.Text, tbPassword.Text, tbEmail.Text, tbPhoneNumber.Text, "Продавец", tbPassportData.Text, tbITN.Text);
                Database.ChangeInformationOfUser(user);
                CurrentUser.User = user;
                HidingFieldChanges();
            }
        }
        private bool CheckPnoneNumber(string phoneNumber)
        {
            string pattern = "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$";
            Match isMatch = Regex.Match(phoneNumber, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        private bool CheckITN(string itn)
        {
            return itn.Length == 12 && long.TryParse(itn, out var num);
        }
        private bool CheckPasportData(string itn)
        {
            return itn.Length == 10 && long.TryParse(itn, out var num);
        }

        private void HidingFieldChanges()
        {
            tbFullName.IsEnabled = false;
            tbPhoneNumber.IsEnabled = false;
            tbPassword.IsEnabled = false;
            tbITN.IsEnabled = false;
            tbPassportData.IsEnabled = false;
            tbPassword.Text = "";

            for (int i = 0; i < CurrentUser.User.Password.Length; i++)
            {
                tbPassword.Text += '*';
            }
        }

        private void CreateApllication(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductSellApllicationPage());
        }

        private void GoToSellsStatistic(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SalesStatisticPage());
        }
    }
}
