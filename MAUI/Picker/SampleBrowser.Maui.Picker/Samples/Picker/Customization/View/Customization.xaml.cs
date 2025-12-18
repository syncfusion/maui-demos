#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Picker.SfPicker;

public partial class Customization : SampleView
{
    public Customization()
    {
        InitializeComponent();
        this.Picker.HeaderView.Height = 50;
        this.Picker.HeaderView.Text = "Select a Color";

        this.Picker1.HeaderView.Height = 50;
        this.Picker1.HeaderView.Text = "Select a Color";

        this.Picker.SelectionView.Padding = 0;
        this.Picker.SelectionView.CornerRadius = 0;

        this.Picker1.SelectionView.Padding = 0;
        this.Picker1.SelectionView.CornerRadius = 0;
    }
}