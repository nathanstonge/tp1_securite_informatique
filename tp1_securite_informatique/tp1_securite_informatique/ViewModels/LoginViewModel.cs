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
        //public Models.User User { get; set; }
        private string _username;
        private string _password;
        private int? _userIdFound;

        public LoginViewModel(LoginPage page)
        {
            _db = new Models.AuthCredentialsDbContext();
            _page = page;
            _window = Application.Current.MainWindow;
            //User = new User();
            _username = _page.Username.ToString();
            _password = _page.Password.ToString();
            _userIdFound = null;


            ConnexionCommand = new RelayCommand(
                o => CredentialsVerification(_username, _password),
                o => Connexion());
        }

        public ICommand ConnexionCommand { get; set;}
        public void Connexion()
        {
            (_window as MainWindow).TopBar.Content = "Generation du code OTP";
            //Parametre OTPCodePage: id utilisateur trouve
            _page.NavigationService.Navigate(new OTPCodePage(1));
        }

        //A completer
        public bool CredentialsVerification(string username, string password)
        {
            //Code pour authentifier un utilisateur dans la collection
            //Assigner _userIdFound si authentification reussie
            return true;
        }
    }
}
