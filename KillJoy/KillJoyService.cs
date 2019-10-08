using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillJoy
{
    public class KillJoyService
    {

        public static KillJoyService Instance;
        public bool FocusStarted = false;

        public KillJoyService()
        {
            Instance = this;
        }

    }
}
