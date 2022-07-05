#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace SampleBrowser.Maui.SfScheduler;
using SampleBrowser.Maui.Core;

/// <summary>
/// Interaction logic for AppearanceCustomization.xaml
/// </summary>
public partial class AppearanceCustomization : SampleView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppearanceCustomization" /> class.
    /// </summary>
    public AppearanceCustomization()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Scheduler.Handler?.DisconnectHandler();
    }
}