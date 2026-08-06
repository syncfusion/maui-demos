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
