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
using Atlas.UI;

namespace KillJoy
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsPopup
    {

        public List<RunningProcess> Processes = new List<RunningProcess>();

        public SettingsPopup()
        {
            InitializeComponent();
            this.ProcessGrid.DataContext = Processes;
            new SettingsHandler(this);
        }

        private void myDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // stub
        }

        private void BlockToggle(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default["BlackList"];
        }
    }
}
