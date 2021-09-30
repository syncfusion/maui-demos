#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

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
