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
using Atlas.UI;

namespace KillJoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public HomePage homeObj;
        public SettingsPopup settingsObj;

        public MainWindow()
        {
            InitializeComponent();
            homeObj = new HomePage();
            this.MainFrame.Navigate(homeObj);
        }

        private void MainWindow_DisplaySettings(object sender, RoutedEventArgs e)
        {
            new SettingsPopup().Show();
        }

        private void MainWindow_DisplayHome(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(homeObj);
        }

    }
}
