using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tp1_securite_informatique_client.Models;
using tp1_securite_informatique_client.Views.Pages;
using tp1_securite_informatique_client.Views.Windows;

namespace tp1_securite_informatique_client.ViewModels
{
    public class LoginViewModel
    {
        public Models.AuthCredentialsDbContext _db;
        private LoginPage _loginPage;
        Window _window;
        private int _userIdFound;

        public LoginViewModel(LoginPage loginPage)
        {
            _db = new Models.AuthCredentialsDbContext();
            _loginPage = loginPage;
            _window = Application.Current.MainWindow;

            ConnexionCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(_loginPage.Username.Text) && !string.IsNullOrEmpty(_loginPage.Password.Password),
                o => Connexion(_loginPage.Username.Text.ToString(), _loginPage.Password.Password.ToString()));

        }

        public ICommand ConnexionCommand { get; private set;}

        //Gestion de la connexion de l'utilisateur
        public void Connexion(string username, string password)
        {
            foreach (User user in _db.Users)
            {
                if (username == user.Username)
                {
                    if (password == user.Password)
                    {
                        _userIdFound = user.Id;
                        (_window as MainWindow).TopBar.Content = "Generation du code OTP";
                        _loginPage.NavigationService.Navigate(new OTPCodePage(_userIdFound));
                    }
                    else
                    {
       
                        _loginPage.Fail.Visibility = Visibility.Visible;
                    }
                }

            }
            _loginPage.Fail.Visibility = Visibility.Visible;
        }
    }
}
