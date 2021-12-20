using Microsoft.Maui.Controls;
using Syncfusion.Maui.Core.Internals;
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

        public event EventHandler<TouchEventArgs>? Touched;

        public CustomGrid()
        {
            this.AddTouchListener(this);
        }

        public void OnTouch(TouchEventArgs e)
        {
            this.Touched?.Invoke(this, e);
        }
    }
}
