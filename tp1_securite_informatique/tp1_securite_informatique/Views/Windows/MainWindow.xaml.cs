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
using System.Windows.Shapes;
using tp1_securite_informatique_client.Views.Pages;

namespace tp1_securite_informatique_client.Views.Windows
{
    
    public partial class MainWindow : Window
    {
        Page _loginPage;
        public MainWindow()
        {
            _loginPage = new LoginPage();
            InitializeComponent();
            Main.Content = _loginPage;
            TopBar.Content = "Authentification";
        }
    }
}
