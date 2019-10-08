using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillJoy
{
    public class SettingsHandler
    {

        public static SettingsHandler Instance;

        public SettingsHandler(SettingsPopup myWindow)
        {
            Instance = this;
            Process[] opList = ProcessDiscovery.GetAllWindowedProcesses();
            RunningProcess[] pList = ProcessDiscovery.ProcessesToRPArray(opList);
            myWindow.Processes.AddRange(pList);
        }

    }
}
