using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tp1_securite_informatique_client.Models
{
    public class User
    {
        //public event PropertyChangedEventHandler? PropertyChanged;
        //public int Id { get; set; }
        //private string _username;
        //public string Username
        //{
        //    get { return _username; }
        //    set
        //    {
        //        if (_username != value)
        //        {
        //            _username = value;
        //            SetIsValid();
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //private string _password;
        //public string Password
        //{
        //    get { return _password; }
        //    set
        //    {
        //        if (_password != value)
        //        {
        //            _password = value;
        //            SetIsValid();
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //private bool _isValid;

        //public bool IsValid
        //{
        //    get { return _isValid; }
        //}
        //private void SetIsValid()
        //{

        //    _isValid = !string.IsNullOrEmpty(Username)
        //            && !string.IsNullOrEmpty(Password);

        //}
        //private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
        //public User()
        //{

        //}
    }
}
