using Microsoft.Maui.Controls;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Core
{
    internal class CustomGrid : Grid, ITouchListener
    {

        public event EventHandler<TouchListenerEventArgs>? Touched;

        public CustomGrid()
        {
            new TouchListener(this);
        }

        public void OnTouchAction(TouchListenerEventArgs e)
        {
            this.Touched?.Invoke(this, e);
        }
    }
}
