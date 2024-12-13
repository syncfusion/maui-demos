#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Popup;
using Syncfusion.Maui.Themes;
using System;
using System.Globalization;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Swiping : SampleView
{


    public Swiping()
    {
        InitializeComponent();
    }


    private void TapGestureRecognizer_EditButtonTapped(object sender, TappedEventArgs e)
    {
        swipingBehavior.EditButtonPressed();
    }

    private void TapGestureRecognizer_DeleteButtonTapped(object sender, TappedEventArgs e)
    {
        swipingBehavior.DeleteButtonPressed();
    }

}