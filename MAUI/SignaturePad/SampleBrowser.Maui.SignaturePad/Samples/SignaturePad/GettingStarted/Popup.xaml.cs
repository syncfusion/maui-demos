#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SignaturePad.SfSignaturePad;

public partial class Popup : ContentView
{
    public static readonly BindableProperty IsOpenProperty =
          BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(Popup),
              false, propertyChanged: OnPopupStateChanged);

    private bool hasSignature;

    public Popup()
    {
        Opacity = 0;
        InputTransparent = true;

        InitializeComponent();
    }

    public bool IsOpen
    {
        get { return (bool)GetValue(IsOpenProperty); }
        set { SetValue(IsOpenProperty, value); }
    }

    private static void OnPopupStateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((Popup)bindable).OnPopupStateChanged((bool)newValue);
    }

    private async void OnPopupStateChanged(bool newValue)
    {
        double opacity;
        if (newValue)
        {
            opacity = 1;
            InputTransparent = false;
        }
        else
        {
            opacity = 0;
            InputTransparent = true;
            hasSignature = false;
            SignaturePad.Clear();
        }

        await this.FadeTo(opacity, 100, Easing.SinOut);
    }

    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        hasSignature = false;
        SignaturePad.Clear();
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is InvoiceViewModel viewModel)
        {
            viewModel.LabelOpacity = hasSignature ? 0 : 1;
            viewModel.ImageSource = SignaturePad.ToImageSource();

            IsOpen = false;
        }
    }

    private void OnCloseButtonClicked(object sender, EventArgs e)
    {
        IsOpen = false;
    }

    private void OnDrawStarted(object sender, System.ComponentModel.CancelEventArgs e)
    {
        hasSignature = true;
    }
}