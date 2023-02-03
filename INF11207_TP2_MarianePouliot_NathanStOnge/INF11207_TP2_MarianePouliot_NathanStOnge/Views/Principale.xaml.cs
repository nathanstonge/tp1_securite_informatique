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

namespace INF11207_TP2_MarianePouliot_NathanStOnge
{
    /// <summary>
    /// Logique d'interaction pour Principale.xaml
    /// </summary>
    public partial class Principale : Window
    {
        public Principale(int currentUserId)
        {
            InitializeComponent();
            DataContext = new ViewModels.SettingViewModel(currentUserId, this);
        }
    }
}
