using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace KillJoy
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 

    public class SimpleCommand : ICommand
    {
        private Action<object> _action;

        public event EventHandler CanExecuteChanged;

        public SimpleCommand(Action<object> action)
        {
            Debug.Assert(action != null);
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }

    public partial class SettingsPopup
    {

        public List<RunningProcess> Processes { get; set; }
        public ICommand CheckedChangedCommand { get; }

        public SettingsPopup()
        {
            InitializeComponent();
            DataContext = this;
            Processes = new List<RunningProcess>();

            CheckedChangedCommand = new SimpleCommand((obj) =>
            {
                var process = obj as RunningProcess;

                BlockToggle(process.Name);
            });

            new SettingsHandler(this);
        }

        private void BlockToggle(string name)
        {
            SettingsHandler.ToggleKeySettingsBlacklist(name);
            Debug.WriteLine(SettingsHandler.GetSettingsBlacklist());
        }
    }
}
