#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion


using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.BadgeView.SfBadgeView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Offset : SampleView
    {
        public Offset()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (this.badgeView.BadgeSettings != null)
            {
                var oldValue = this.badgeView.BadgeSettings.Offset;
                this.badgeView.BadgeSettings.Offset = new Point(e.NewValue, oldValue.Y);
            }
        }

        private void Slider_ValueChanged1(object sender, ValueChangedEventArgs e)
        {
            if (this.badgeView.BadgeSettings != null)
            {
                var oldValue = this.badgeView.BadgeSettings.Offset;
                this.badgeView.BadgeSettings.Offset = new Point(oldValue.X, e.NewValue);
            }
        }
    }
}