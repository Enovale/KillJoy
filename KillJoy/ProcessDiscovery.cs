using System.Diagnostics;

#nullable enable
namespace KillJoy
{
    /// <summary>
    /// Full of methods to discover, kill, and track running programs on the user's PC.
    /// </summary>
    public static class ProcessDiscovery
    {

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

    }
}
