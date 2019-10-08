using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable enable
namespace KillJoy
{

    public class RunningProcess
    {

        public string Name { get; set; }
        public string Title { get; set; }
        public int PID { get; set; }
        public string Path { get; set; }
        public bool Blocked { get; set; }

        public RunningProcess(Process task, bool b = false)
        {
            this.Name = task.ProcessName;
            this.Title = task.MainWindowTitle ?? task.ProcessName;
            this.PID = task.Id;
            this.Path = "";
            this.Blocked = b;
        }

    }

    /// <summary>
    /// Full of methods to discover, kill, and track running programs on the user's PC.
    /// </summary>
    public static class ProcessDiscovery
    {

        public static RunningProcess[] ProcessesToRPArray(Process[] inArray)
        {
            RunningProcess[] processes = new RunningProcess[inArray.Length];
            for(var i = 0; i < inArray.Length; i++)
            {
                processes[i] = new RunningProcess(inArray[i]);
            }
            return processes;
        }

        /// <summary>
        /// Finds all Process objects based on a given name and kills them.
        /// </summary>
        /// <param name="name">The name of the processes you want to find.</param>
        /// <returns></returns>
        public static bool KillProcessesByName(string name)
        {
            Process[] processList = Process.GetProcessesByName(name);
            if (processList.Length == 0) return false;
            foreach (var process in Process.GetProcessesByName(name))
            {
                process.Kill();
            }
            return true;
        }

        /// <summary>
        /// Finds the first Process object based on a given name and kills it.
        /// </summary>
        /// <param name="name">The name of the process you want to find.</param>
        /// <returns>Returns True if the process was found and killed.</returns>
        public static bool KillProcessByName(string name)
        {
            Process[] processList = Process.GetProcessesByName(name);
            if (processList.Length == 0) return false;
            processList[0].Kill();
            return true;
        }

        /// <summary>
        /// Returns an array of Process's by name
        /// </summary>
        /// <param name="name">The processes to find by name/</param>
        /// <returns>Returns a Process[] object of the process found, otherwise null</returns>
        public static Process[]? GetProcessesByName(string name)
        {
            Process[] processList = Process.GetProcessesByName(name);
            if (processList.Length == 0) return null;
            return processList;
        }

        /// <summary>
        /// Returns the first process found from a given name
        /// </summary>
        /// <param name="name">The process to find by name/</param>
        /// <returns>Returns a Process object of the process found, otherwise null</returns>
        public static Process? GetProcessByName(string name)
        {
            Process[] processList = Process.GetProcessesByName(name);
            if (processList.Length == 0) return null;
            return processList[0];
        }

        /// <summary>
        /// Returns an array of every process that has a window
        /// </summary>
        /// <returns>Array of all windowed processes</returns>
        public static Process[] GetAllWindowedProcesses()
        {
            List<Process> finalList = new List<Process>();
            Process[] tempList = Process.GetProcesses();

            foreach (Process process in tempList)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    finalList.Add(process);
                }
            }
            return finalList.ToArray();
        }

    }
}
