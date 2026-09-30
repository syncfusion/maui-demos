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


    private void TapGestureRecognizer_EditButtonTapped(object? sender, TappedEventArgs e)
    {
        swipingBehavior.EditButtonPressed();
    }

    private void TapGestureRecognizer_DeleteButtonTapped(object? sender, TappedEventArgs e)
    {
        swipingBehavior.DeleteButtonPressed();
    }

}