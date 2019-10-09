using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KillJoy
{

    public class RunningProcess
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int PID { get; set; }
        public string Path { get; set; }
        public bool Blocked { get; set; }

        public RunningProcess(Process task)
        {
            Name = task.ProcessName;
            Title = task.MainWindowTitle ?? task.ProcessName;
            if(Title == "")
            {
                Title = task.ProcessName;
            }
            PID = task.Id;
            Path = task.GetMainModuleFileName();
            Blocked = SettingsHandler.GetKeySettingsBlacklist(task.ProcessName);
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
            for (var i = 0; i < inArray.Length; i++)
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
        public static Process[] GetProcessesByName(string name)
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
        public static Process GetProcessByName(string name)
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
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            var ret = Process.GetProcesses()
                 .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle))
                 .ToArray();

            // Stop timing.
            stopwatch.Stop();

            // Write result.
            Debug.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            return ret;
        }
    }

    internal static class Extensions
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            var fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            try
            {
                return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                    fileNameBuilder.ToString() :
                    null;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return null;
            }
            catch(System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
