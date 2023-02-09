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
using System.Windows.Threading;

namespace tp1_securite_informatique_serveur.ViewModels
{
    public class LoginViewModel
    {
        public Models.AuthCredentialsDbContext _db;
        private LoginPage _loginPage;
        private string _otpCode;
        private string _oldOtpCodeFred;
        private string _oldOtpCodeMariane;
        private string _oldOtpCodeNath;
        private DateTime _dateTime;
        private DateTime _dateTimeMinusOneMinute;
        private int _userIdFound;
        private int _loginAttempts = 0;
        DispatcherTimer _dispatcherTimer;

        public LoginViewModel(LoginPage loginPage)
        {
            //Affichage des codes OTP valides du dernier bloc de 60 secondes
            _db = new Models.AuthCredentialsDbContext();
            _loginPage = loginPage;
            _oldOtpCodeMariane = OTPGenerator.OTPGenerator.Generate(getFormattedDateTimeMinusOneMinute(), 1);
            _oldOtpCodeFred = OTPGenerator.OTPGenerator.Generate(getFormattedDateTimeMinusOneMinute(), 2);
            _oldOtpCodeNath = OTPGenerator.OTPGenerator.Generate(getFormattedDateTimeMinusOneMinute(), 3);
            _loginPage.OTPCode.Text = $"Anciens codes | mariane: {_oldOtpCodeMariane} | fred: {_oldOtpCodeFred} | nath: {_oldOtpCodeNath}";

            //Paramétrisation du compte à rebours
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();

            ConnexionCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(_loginPage.Username.Text) && !string.IsNullOrEmpty(_loginPage.Password.Password) && !string.IsNullOrEmpty(_loginPage.OTPCode.Text),
                o => Connexion(_loginPage.Username.Text.ToString(), _loginPage.Password.Password.ToString()));

        }
        //Mise à jour de l'affichage des codes OTP valides du dernier bloc de 60 secondes
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (60 - DateTime.Now.Second == 1 && _loginPage.OTPCode.Text == "") {
                _oldOtpCodeMariane = OTPGenerator.OTPGenerator.Generate(getFormattedDateTimeMinusOneMinute(), 1);
                _oldOtpCodeFred = OTPGenerator.OTPGenerator.Generate(getFormattedDateTimeMinusOneMinute(), 2);
                _oldOtpCodeNath = OTPGenerator.OTPGenerator.Generate(getFormattedDateTimeMinusOneMinute(), 3);
                _loginPage.OTPCode.Text = $"Anciens codes | mariane: {_oldOtpCodeMariane} | fred: {_oldOtpCodeFred} | nath: {_oldOtpCodeNath}";
            }

        }
        public ICommand ConnexionCommand { get; private set; }
        //Gestion de la connexion de l'utilisateur
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
        //Méthode retournant l'heure UTC actuelle dans le format "dd-MM-yyyy-HH-mm"
        private string getFormattedDateTime()
        {
            _dateTime = DateTime.UtcNow;
            return _dateTime.ToString("dd-MM-yyyy-HH-mm");
        }
        //Méthode retournant l'heure UTC actuelle soustraite de 58 secondes dans le format "dd-MM-yyyy-HH-mm"
        private string getFormattedDateTimeMinusOneMinute()
        {
            _dateTimeMinusOneMinute = DateTime.UtcNow.AddSeconds(-58);
            return _dateTimeMinusOneMinute.ToString("dd-MM-yyyy-HH-mm");
        }
        //Méthode cachant les messages d'erreur et de succès par rapport à l'authentification
        private void resetLoginStatusMessagesVisibility()
        {
            _loginPage.Fail.Visibility = Visibility.Hidden;
            _loginPage.Success.Visibility = Visibility.Hidden;

        }
        //Méthode vérifiant le nombre de tentatives de connexion (si ce nombre s'avère à être égal à 5, l'application serveur se fermera)
        private void loginAttemptsVerification()
        {
            if(_loginAttempts == 5)
                System.Windows.Application.Current.Shutdown();
        }
    }
}
