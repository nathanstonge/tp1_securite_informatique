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
using tp1_securite_informatique_client.Views.Windows;
using static System.Collections.Specialized.BitVector32;

namespace tp1_securite_informatique_client.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Window _window;
        public LoginPage()
        {
            _window = Application.Current.MainWindow;
            InitializeComponent();
            DataContext = new ViewModels.LoginViewModel(this);
            //(_window as MainWindow).TopBar.Content = "Authentification";
        }
    }
}
