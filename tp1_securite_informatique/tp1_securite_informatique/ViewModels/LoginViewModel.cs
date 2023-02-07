using INF11207_TP2_MarianePouliot_NathanStOnge.ViewModels;
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
        private int _userIdFound;
        //private bool _reponse;

        public LoginViewModel(LoginPage page)
        {
            _db = new Models.AuthCredentialsDbContext();
            _page = page;
            _window = Application.Current.MainWindow;
            //_username = _page.Username.Text.ToString();
            //_password = _page.Password.Text.ToString();
            //User user = new User(100, _username, _password);

            //ConnexionCommand = new RelayCommand(
                //o => CredentialsVerification(_username, _password),
                //o => Connexion());

            ConnexionCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(_page.Username.Text) && !string.IsNullOrEmpty(_page.Password.Text),
                o => Connexion(_page.Username.Text.ToString(), _page.Password.Text.ToString()));

        }

        public ICommand ConnexionCommand { get; private set;}
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
                        //Parametre OTPCodePage: id utilisateur trouve
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

        //A completer
        //public bool CredentialsVerification(string username, string password)
        //{
            
        //    foreach (User user in _db.Users)
        //    {
        //        if (username == user.Username)
        //        {
        //            if (password == user.Password)
        //            {
        //                _reponse = true;
        //                _userIdFound = user.Id;
        //            }
        //            else
        //            {
        //                _reponse = false;
        //                _page.Echec.Content = "Informations incorrectes pour la connexion";
        //            }
        //        }
        //        else
        //        {
        //            _reponse = false;
        //            _page.Echec.Content = "Informations incorrectes pour la connexion";
        //        }

        //    }
        //    return _reponse;

        //    //Code pour authentifier un utilisateur dans la collection
        //    //Assigner _userIdFound si authentification reussie
        //}
    }
}
