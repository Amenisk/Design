using Design.Pages;
using MarketplaceKazonberriesExpress.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Marketplace_design.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private string _role;
        public RegistrationPage(string role)
        {
            InitializeComponent();
            _role = role;
            if (_role == "Покупатель")
            {
                tblITN.Visibility = Visibility.Hidden;
                tblPasportData.Visibility = Visibility.Hidden;
                tbITN.Visibility = Visibility.Hidden;
                tbPasportData.Visibility = Visibility.Hidden;
            }
            else
            {
                tblITN.Visibility = Visibility.Visible;
                tblPasportData.Visibility = Visibility.Visible;
                tbITN.Visibility = Visibility.Visible;
                tbPasportData.Visibility = Visibility.Visible;
            }
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(tbEmail.Text != "" && tbPhoneNumber.Text != "" && tbFullName.Text != "" && tbPassword.Text != "")
            {
                if (_role == "Продавец" && tbITN.Text == "" && tbPasportData.Text == "")
                {
                    MessageBox.Show("Заполните все поля для регистрации!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(!CheckEmail(tbEmail.Text))
                {
                    MessageBox.Show("Некорректный Email!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(!CheckPnoneNumber(tbPhoneNumber.Text)) 
                {
                    MessageBox.Show("Некорректный номер телефона!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(Database.CheckEmail(tbEmail.Text))
                {
                    MessageBox.Show("Пользователь с таким адресом электронной почты уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(tbEmail.Text.Length > 30)
                {
                    MessageBox.Show("Длина Email не должна быть больше 30 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (tbFullName.Text.Length > 30)
                {
                    MessageBox.Show("Длина имени не должна быть больше 30 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (tbPassword.Text.Length > 20)
                {
                    MessageBox.Show("Длина пароля не должна быть больше 20 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_role == "Продавец")
                {
                    if(!CheckITN(tbITN.Text)) 
                    {
                        MessageBox.Show("Некорректный ИНН!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (!CheckPasportData(tbPasportData.Text))
                    {
                        MessageBox.Show("Некорректные паспортные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Database.SaveUserToDB(new User(tbFullName.Text, tbPassword.Text, tbEmail.Text, tbPhoneNumber.Text, "Продавец", tbPasportData.Text, tbITN.Text));
                    NavigationService.Navigate(new MainPage());
                    return;
                }

                NavigationService.Navigate(new MainPage());
                Database.SaveUserToDB(new User(tbFullName.Text, tbPassword.Text, tbEmail.Text, tbPhoneNumber.Text, "Покупатель"));
            }
            else
            {
                MessageBox.Show("Заполните все поля для регистрации!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }

        private bool CheckEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
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
    }
}
