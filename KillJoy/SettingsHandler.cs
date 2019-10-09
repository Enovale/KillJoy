using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Collections;

namespace KillJoy
{
    public class SettingsHandler
    {

        public static SettingsHandler Instance;
        private SettingsPopup myWindow;

        public SettingsHandler(SettingsPopup myW)
        {
            Instance = this;
            myWindow = myW;
            myWindow.Processes = GenerateProcessList();
            UpdateLoop();
        }

        private async void UpdateLoop()
        {
            await Task.Run(() => UpdateProcessList());
            myWindow.ProcessGrid.ItemsSource = myWindow.Processes;
            await Task.Delay(3000);
            UpdateLoop();
        }

        internal void UpdateProcessList()
        {
            myWindow.Processes = GenerateProcessList();
        }

        public List<RunningProcess> GenerateProcessList()
        {

            Process CurrentPro = new Process();

            CurrentPro = Process.GetCurrentProcess();

            Process[] opArray = ProcessDiscovery.GetAllWindowedProcesses();
            List<Process> opList = opArray.ToList<Process>();
            for(var i = 0; i < opList.Count; i++)
            {
                if(opList[i].Id == CurrentPro.Id)
                {
                    opList.Remove(opList[i]);
                }
            }

            RunningProcess[] pList = ProcessDiscovery.ProcessesToRPArray(opList.ToArray());
            return pList.ToList<RunningProcess>();
        }

        public static Dictionary<string, bool> GetSettingsBlacklist()
        {
            string settingsString = Properties.Settings.Default["BlackList"] as string;
            Dictionary<string, bool> deserializedProduct = JsonConvert.DeserializeObject<Dictionary<string, bool>>(settingsString);
            if(deserializedProduct == null)
            {
                deserializedProduct = DefaultSettingsBlacklist();
            }
            return deserializedProduct;
        }

        public static bool GetKeySettingsBlacklist(string key)
        {
            string settingsString = Properties.Settings.Default["BlackList"] as string;
            Dictionary<string, bool> deserializedProduct = JsonConvert.DeserializeObject<Dictionary<string, bool>>(settingsString);
            if (deserializedProduct == null)
            {
                deserializedProduct = DefaultSettingsBlacklist();
            }
            if (!deserializedProduct.ContainsKey(key))
            {
                deserializedProduct.Add(key, false);
                SetSettingsBlacklist(deserializedProduct);
            }
            return deserializedProduct[key];
        }

        public static void SetSettingsBlacklist(Dictionary<string, bool> settingToWrite)
        {
            Properties.Settings.Default["BlackList"] = JsonConvert.SerializeObject(settingToWrite);
            Properties.Settings.Default.Save();
        }

        public static void SetKeySettingsBlaclist(string key, bool blocked)
        {
            Dictionary<string, bool> des = GetSettingsBlacklist();
            if (!des.ContainsKey(key))
            {
                des.Add(key, false);
            }
            else
            {
                des[key] = blocked;
            }
            SetSettingsBlacklist(des);
        }

        public static void ToggleKeySettingsBlacklist(string key)
        {
            Dictionary<string, bool> des = GetSettingsBlacklist();
            if (!des.ContainsKey(key))
            {
                des.Add(key, false);
            } else
            {
                des[key] = !des[key];
            }
            SetSettingsBlacklist(des);
        }

        public static Dictionary<string, bool> DefaultSettingsBlacklist()
        {
            Dictionary<string, bool> defaultBlacklist = new Dictionary<string, bool>();
            Properties.Settings.Default["BlackList"] = JsonConvert.SerializeObject(defaultBlacklist);
            return defaultBlacklist;
        }

    }
}
