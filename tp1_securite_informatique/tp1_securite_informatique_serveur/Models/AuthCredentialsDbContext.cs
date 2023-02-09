using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp1_securite_informatique_serveur.Models
{
    //Classe regroupant les données des utilisateurs à exploiter dans le cadre de cette application
    public class AuthCredentialsDbContext
    {
        public ObservableCollection<User> Users { get; set; }

        public AuthCredentialsDbContext()
        {
            Users = new ObservableCollection<User>()
            {
                new User(1, "mariane", "123Soleil"),
                new User(2, "fred", "admin123"),
                new User(3, "nath", "Copine123")
            };
        }
    }
}
