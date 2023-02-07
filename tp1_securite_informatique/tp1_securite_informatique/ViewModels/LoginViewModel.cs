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
        private LoginPage _page;
        Window _window;
        private string _username;
        private string _password;
        private int _userIdFound;

        public LoginViewModel(LoginPage page)
        {
            _db = new Models.AuthCredentialsDbContext();
            _page = page;
            _window = Application.Current.MainWindow;

            ConnexionCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(_page.Username.Text) && !string.IsNullOrEmpty(_page.Password.Password),
                o => Connexion(_page.Username.Text.ToString(), _page.Password.Password.ToString()));

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
                        _page.NavigationService.Navigate(new OTPCodePage(_userIdFound));
                    }
                    else
                    {
       
                        _page.Echec.Content = "Informations incorrectes pour la connexion";
                    }
                }
                else
                {
                    _page.Echec.Content = "Informations incorrectes pour la connexion";
                }

            }
        }
    }
}
