using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.Model
{   
    public class Applicationlist
    {
        private ImageSource? appimage;
        public ImageSource? AppImage
        {
            get { return appimage; }
            set
            {
                appimage = value;
            }
        }
        private string name = string.Empty;
        public string AppName
        {
            get { return name; }
            set { name = value; }
        }
        private string date = string.Empty;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
