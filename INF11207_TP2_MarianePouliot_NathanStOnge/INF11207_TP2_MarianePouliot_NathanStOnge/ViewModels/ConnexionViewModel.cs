using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace INF11207_TP2_MarianePouliot_NathanStOnge.ViewModels
{
    internal class ConnexionViewModel
    {
        private Connexion _connexionWindow;
        private Models.WineQualityDbContext _db;
        public ObservableCollection<Models.User> Users { get; private set; }
        

        public ConnexionViewModel(Connexion connexionWindow)
        {
            _connexionWindow = connexionWindow;
            _db = new Models.WineQualityDbContext();
            Users = new ObservableCollection<Models.User>();
            

            Refresh();

            CreateUserCommand = new RelayCommand(
                o => true,
                o => CreateUser()

                );
            ConnexionCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(connexionWindow.cbUser.Text.Split(" - ")[0]),
                o=> Connexion((_connexionWindow.cbUser.SelectedItem as Models.User).UserId)
                );
            QuitCommand = new RelayCommand(
                o => true,
                o => Quit()
                );


        }

        public ICommand RefreshCommand { get; private set; }

        private void Refresh()
        {
            Users.Clear();
            foreach (Models.User user in _db.Users.ToList())
            {
                Users.Add(user);
            }
        }

        public ICommand CreateUserCommand { get; private set; }
        private void CreateUser()
        {
            Formulaire formulaireWindow = new Formulaire();
            this._connexionWindow.Close();
            formulaireWindow.Show();
        }

        public ICommand ConnexionCommand { get; private set; }

        private void Connexion(int userId)
        {
            
            Principale principaleWindow = new Principale(userId);
            this._connexionWindow.Close();
            principaleWindow.Show();
           
        }
        public ICommand QuitCommand { get; private set; }
        private void Quit()
        {
            Application.Current.Shutdown();
        }
    }
   
}
