using System;

namespace KillJoy
{
    public class KillJoyService
    {
        private static KillJoyService _instance;

        public static KillJoyService Instance =>
            new Lazy<KillJoyService>(() => _instance ?? (_instance = new KillJoyService()), true).Value;

        public bool FocusStarted { get; set; }

        private KillJoyService()
        {
            FocusStarted = false;
        }
    }
}
