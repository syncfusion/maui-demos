#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Calendar;

namespace SampleBrowser.Maui.Calendar.SfCalendar
{
    /// <summary>
    /// Interaction logic for GettingStarted.xaml
    /// </summary>
    public partial class Decade : SampleView
    {
        /// <summary>
        /// Background color for border control. Border control will not shown on mobile platforms.
        /// </summary>
        public Color BGColor { get; set; }

        /// <summary>
        /// Stroke color for border control. Border control will not shown on mobile platforms.
        /// </summary>
        public Color StrokeColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decade" /> class.
        /// </summary>
        public Decade()
        {
            InitializeComponent();
#if MACCATALYST || (!ANDROID && !IOS)
            this.Background = Brush.WhiteSmoke;
            this.Margin = new Thickness(-4, -4, -6, -6);
            this.BGColor = Colors.White;
            this.StrokeColor = Color.FromArgb("#E6E6E6");
#else
            this.BGColor = Colors.Transparent;
            this.StrokeColor = Colors.Transparent;
#endif

            this.BindingContext = this;
#if IOS || MACCATALYST
            this.border.IsVisible = true;
            this.decade1.SelectedDate = DateTime.Now;
#else
            this.frame.IsVisible = true;
            this.decade.SelectedDate = DateTime.Now;
#endif
        }
    }
}