using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace INF11207_TP2_MarianePouliot_NathanStOnge.Models
{ 
    internal class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public User()
        {
            Predictions = new List<Prediction>();
        }

        private bool _isValid;

        Regex emailFormat = new Regex(@"^[\w\.]+@([\w,]+\.)+[\w-]{2,4}$");

        public bool IsValid
        {
            get { return _isValid; }
        }

        
        public int UserId { get; set; }
        
        public ICollection<Prediction>? Predictions { get; set; }

        private string? _name;
        public string? Name {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    SetIsValid();
                    OnPropertyChanged();
                }
            }
        }

        private string? _firstName;
        public string? FirstName {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    SetIsValid();
                    OnPropertyChanged();
                }
            }
        }

        private string? _city;
        public string? City {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    SetIsValid();
                    OnPropertyChanged();
                }
            }
        }

        private string? _gender;
        public string? Gender {
            get { return _gender; }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    SetIsValid();
                    OnPropertyChanged();
                }
            }
        }

        private string? _birthday;
        public string? Birthday {
            get {
                return _birthday; 
            }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    SetIsValid();
                    OnPropertyChanged();
                }
            }
        }

        private string? _email;
        public string? Email {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    SetIsValid();
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetIsValid()
        {

            _isValid = !string.IsNullOrEmpty(Name)
                    && !string.IsNullOrEmpty(FirstName)
                    && !string.IsNullOrEmpty(Email)
                    && emailFormat.IsMatch(Email)
                    && !string.IsNullOrEmpty(Birthday);

        }
    }
}
