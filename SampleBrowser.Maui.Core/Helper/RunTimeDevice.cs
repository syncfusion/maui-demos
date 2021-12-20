using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Core
{
    public static class RunTimeDevice
    {
        public static string PlatformInfo;

        public static bool IsMobileDevice()
        {
            if (PlatformInfo == "Android" || PlatformInfo == "iOS")
                return true;

            return false;
        }
    }

    public partial class RunTimeDeviceInfo
    {
        
    }
}
