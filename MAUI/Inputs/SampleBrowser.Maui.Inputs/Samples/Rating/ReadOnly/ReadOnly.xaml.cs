using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ProgressBar;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleBrowser.Maui.Inputs.SfRating
{

    public partial class ReadOnly : SampleView
    {
        public ReadOnly()
        {
            InitializeComponent();

#if ANDROID || IOS
            this.Content = new ReadOnlyMobile();
#else
            this.Content = new ReadOnlyDesktop();
#endif
        }
    }
}



