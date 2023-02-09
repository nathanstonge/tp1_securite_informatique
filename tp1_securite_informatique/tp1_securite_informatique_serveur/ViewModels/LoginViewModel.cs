using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tp1_securite_informatique_serveur.Models;
using tp1_securite_informatique_serveur.Views.Pages;
using OTPGenerator;

namespace tp1_securite_informatique_serveur.ViewModels
{
    public class LoginViewModel
    {
        public Models.AuthCredentialsDbContext _db;
        private LoginPage _loginPage;
        private string _otpCode;
        private DateTime _dateTime;
        private int _userIdFound;
        private int _loginAttempts = 0;

        public LoginViewModel(LoginPage loginPage)
        {
            _db = new Models.AuthCredentialsDbContext();
            _loginPage = loginPage;

            ConnexionCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(_loginPage.Username.Text) && !string.IsNullOrEmpty(_loginPage.Password.Password) && !string.IsNullOrEmpty(_loginPage.OTPCode.Text),
                o => Connexion(_loginPage.Username.Text.ToString(), _loginPage.Password.Password.ToString()));

        }
        public ICommand ConnexionCommand { get; private set; }
        public void Connexion(string username, string password)
        {
            resetLoginStatusMessagesVisibility();
            foreach (User user in _db.Users)
            {
                if (username == user.Username)
                {
                    if (password == user.Password)
                    {
                        _userIdFound = user.Id;
                        _otpCode = OTPGenerator.OTPGenerator.Generate(getFormattedDateTime(), _userIdFound);
                        if(_otpCode == _loginPage.OTPCode.Text)
                        {
                            _loginPage.Success.Visibility = Visibility.Visible;
                            return;
                        }
                    }
                    else
                    {

                        _loginPage.Fail.Visibility = Visibility.Visible;
                        _loginAttempts += 1;
                        loginAttemptsVerification();
                        return;
                    }
                }

            }
            _loginPage.Fail.Visibility = Visibility.Visible;
            _loginAttempts += 1;
            loginAttemptsVerification();

        }
        private string getFormattedDateTime()
        {
            _dateTime = DateTime.UtcNow;
            return _dateTime.ToString("dd-MM-yyyy-HH-mm-ss");
        }
        private void resetLoginStatusMessagesVisibility()
        {
            _loginPage.Fail.Visibility = Visibility.Hidden;
            _loginPage.Success.Visibility = Visibility.Hidden;

        }
        private void loginAttemptsVerification()
        {
            if(_loginAttempts == 5)
                System.Windows.Application.Current.Shutdown();
        }
    }
}
