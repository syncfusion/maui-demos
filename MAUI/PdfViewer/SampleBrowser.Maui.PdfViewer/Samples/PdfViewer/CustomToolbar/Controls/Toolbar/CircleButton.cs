#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer
{
    class CircleButton : Button
    {
        public CircleButton()
        {
            BorderWidth = 1;
            BorderColor = Colors.Transparent;
            Padding = 1;
            Margin = 1;
            HeightRequest = 30;
            WidthRequest = 30;
            CornerRadius = 15;
        }
    }
}
