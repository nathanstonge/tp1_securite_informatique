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

namespace tp1_securite_informatique_client.ViewModels
{
    public class OTPViewModel
    {
        private OTPCodePage _page;
        private int _userId;
        DispatcherTimer _timer;
        TimeSpan _time;
        public OTPViewModel(OTPCodePage page, int userId)
        {
            _page = page;
            _userId = userId;

            StartCountDown();
        }

        public void StartCountDown()
        {
            //Generation code OTP
            _page.OTPCode.Content = OTPGenerator.OTPGenerator.Generate(_userId);

            //Depart compte a rebours
            DateTime _dateTime = DateTime.UtcNow;
            string dateTimeString = _dateTime.ToString("dd-MM-yyyy-HH-mm-ss");

            _time = TimeSpan.FromSeconds(Int32.Parse(dateTimeString.Substring(17)));

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                _page.TimeLeft.Content = _time.ToString("c");
                if (_time == TimeSpan.Zero) StartCountDown();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            
        }
        


    }
}
