using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using OTPGenerator;
using tp1_securite_informatique_client.Views.Pages;
using System.Windows.Threading;
using System.Windows.Input;

namespace tp1_securite_informatique_client.ViewModels
{
    public class OTPViewModel
    {
        DispatcherTimer _dispatcherTimer;
        string _otpCode;
        private OTPCodePage _page;
        private int _userId;

        public OTPViewModel(OTPCodePage page, int userId)
        {
            _page = page;
            _userId = userId;
            _otpCode = OTPGenerator.OTPGenerator.Generate(_userId);
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        //Code executé à chaque seconde
        private void dispatcherTimer_Tick(object sender, EventArgs e)  
        {
            int timeLeft = 60 - DateTime.Now.Second;

            _page.OTPCode.Content = _otpCode;

            _page.TimeLeft.Content = timeLeft.ToString("0:00");

            _page.TimeLeftPg.Visibility= Visibility.Visible;

            _page.TimeLeftPg.Value = ((float)timeLeft * 100) / 60;

            if (60 - DateTime.Now.Second == 1) { _otpCode = OTPGenerator.OTPGenerator.Generate(_userId); }
        }
    }
}
