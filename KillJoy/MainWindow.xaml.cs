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

        private NotifyIcon notifier = new NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();
            homeObj = new HomePage();
            this.MainFrame.Navigate(homeObj);

            this.notifier.MouseDown += new System.Windows.Forms.MouseEventHandler(notifier_MouseDown);
            this.notifier.Icon = KillJoy.Properties.Resources.A;
            this.notifier.Visible = true;
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
            this.UseGlowEffect = false;
        }

        private void MainWindow_DisplaySettings(object sender, RoutedEventArgs e)
        {
            new SettingsPopup().Show();
        }

        private void MainWindow_DisplayHome(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(homeObj);
        }

        void notifier_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                //System.Windows.Controls.ContextMenu menu = (System.Windows.Controls.ContextMenu)this.FindResource("NotifierContextMenu");
                //menu.IsOpen = true;
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

    }
}
