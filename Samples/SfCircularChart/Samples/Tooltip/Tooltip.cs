using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCircularChart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tooltip : SampleView
    {
        public Tooltip()
        {
            InitializeComponent();
        }

        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {
            var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCircularChart;

            content.AnimateSeries();
        }
    }
}
