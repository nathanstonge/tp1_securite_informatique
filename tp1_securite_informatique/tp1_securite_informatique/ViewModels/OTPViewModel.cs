using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
//using OTPGenerator;
using tp1_securite_informatique_client.Views.Pages;
using System.Windows.Threading;
using System.Windows.Input;
using tp1_securite_informatique_client.ViewModels;

namespace tp1_securite_informatique_client.ViewModels
{
    public class OTPViewModel
    {
        DispatcherTimer _dispatcherTimer;
        string _otpCode;
        private OTPCodePage _page;
        private int _userId;
        DateTime _dateTime;

        public OTPViewModel(OTPCodePage page, int userId)
        {
            _page = page;
            _userId = userId;

            //Paramétrisation du compte à rebours
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();

            DeconnexionCommand = new RelayCommand(
                o => true,
                o => Deconnexion());

        }

        public ICommand DeconnexionCommand { get; set; }
        //Gestion de la déconnexion de l'utilisateur
        public void Deconnexion()
        {
            _page.NavigationService.Navigate(new LoginPage());
        }


        //Code executé à chaque seconde - Génération des codes OTP et affichage du compte à rebours
        private void dispatcherTimer_Tick(object sender, EventArgs e)  
        {  

            _page.OTPCode.Content = OTPGenerator.OTPGenerator.Generate(getFormattedDateTime(), _userId);

            _page.TimeLeft.Content = (60 - DateTime.UtcNow.Second).ToString("0:00");

            _page.TimeLeftPg.Visibility= Visibility.Visible;

            _page.TimeLeftPg.Value = ((float)(60 - DateTime.UtcNow.Second) * 100) / 60;

            if (60 - DateTime.Now.Second == 1) { 
                _otpCode = OTPGenerator.OTPGenerator.Generate(getFormattedDateTime(), _userId);
            }
        }
        //Méthode retournant l'heure UTC actuelle dans le format "dd-MM-yyyy-HH-mm"
        private string getFormattedDateTime()
        {
            _dateTime = DateTime.UtcNow;
            return _dateTime.ToString("dd-MM-yyyy-HH-mm");
        }

    }
}
