using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace INF11207_TP2_MarianePouliot_NathanStOnge.ViewModels
{
    internal class UserViewModel
    {
        private Window _window;
        public Models.User User { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        //public ObservableCollection<Models.User> Users { get; private set; }
        private Models.WineQualityDbContext _db;

        public UserViewModel(Window window)
        {

            _window = window;

            _db = new Models.WineQualityDbContext();
            User = new Models.User();
            Cities = new ObservableCollection<string>() { "Rimouski", "Québec", "Lévis" };

            CreateCommand = new RelayCommand(
                o => User.IsValid,
                o => Create()
            );

            ReturnCommand = new RelayCommand(
                o => true,
                o => Return()
            );
            
        }

        public ICommand CreateCommand { get; private set; }

        private void Create()
        {

            _db.Add(new Models.User()
            {
                Name = User.Name,
                FirstName = User.FirstName,
                City = User.City,
                Gender = User.Gender,
                Birthday = User.Birthday,
                Email = User.Email

            });
            _db.SaveChanges();
            MessageBox.Show("Profil créé avec succès. Identifiant unique: " + User.UserId);
            Connexion connectwindow = new Connexion();
            this._window.Close();
            connectwindow.Show();
        }

        public ICommand ReturnCommand { get; private set; }

        private void Return()
        {
            Connexion connectwindow = new Connexion();
            this._window.Close();
            connectwindow.Show();
        }
    }
}
