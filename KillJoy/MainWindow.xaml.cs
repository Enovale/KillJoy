using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
            new Lazy<KillJoyService>();
            this.MainFrame.Navigate(homeObj);

            this.TrayIcon.Icon = Properties.Resources.A;
            this.TrayIcon.CaptureMouse();
            this.TrayIcon.MouseLeftButtonUp += notifier_MouseUp;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            this.UseGlowEffect = true;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            this.UseGlowEffect = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            //this.UseGlowEffect = false;
        }

        private void MainWindow_DisplaySettings(object sender, RoutedEventArgs e)
        {
            new SettingsPopup().Show();
        }

        private void MainWindow_DisplayHome(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(homeObj);
        }

        private void notifier_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                this.Show();
                this.Focus();
            }
        }

        private void Menu_Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Open");
        }

        private void Menu_Close(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Close");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
