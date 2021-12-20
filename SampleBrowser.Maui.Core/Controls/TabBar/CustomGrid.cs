#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

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
