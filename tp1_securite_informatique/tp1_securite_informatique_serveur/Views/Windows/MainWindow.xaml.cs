using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tp1_securite_informatique_serveur.Views.Pages;

namespace tp1_securite_informatique_serveur
{
    public partial class MainWindow : Window
    {
        Page _loginPage;

        public MainWindow()
        {
            _loginPage = new LoginPage();
            InitializeComponent();
            this.Left = 60 + this.Width;
            this.Top = SystemParameters.PrimaryScreenHeight/2 - this.Height/2;
            
            Main.Content = _loginPage;
            TopBar.Content = "Authentification";
        }
    }
}
