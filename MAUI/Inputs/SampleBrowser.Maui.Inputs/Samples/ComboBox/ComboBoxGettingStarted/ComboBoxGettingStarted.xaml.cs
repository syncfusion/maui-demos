#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.Inputs.SfComboBox;

/// <summary>
/// 
/// </summary>
public partial class ComboBoxGettingStarted : SampleView
{
    /// <summary>
    /// 
    /// </summary>
    public List<string>? SizeList { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<string>? ResolutionList { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<string>? OrientationList { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public ComboBoxGettingStarted()
    {
        InitializeComponent();
        this.AddListItems();
        this.Content.BindingContext = this;
    }

    private void AddListItems()
    {
        List<string> sizeList = new List<string>();
        sizeList.Add("100%");
        sizeList.Add("125%");
        sizeList.Add("150% (Recommended)"); 
        sizeList.Add("175%");
        SizeList = sizeList;

        List<string> resolutionList = new List<string>();
        resolutionList.Add("1920 x 1080 (Recommended)");
        resolutionList.Add("1680 x 1050");
        resolutionList.Add("1600 x 900");
        ResolutionList = resolutionList;

        List<string> orientationList = new List<string>();
        orientationList.Add("Landscape");
        orientationList.Add("Portrait");
        orientationList.Add("Landscape (flipped)");
        orientationList.Add("Portrait (flipped)");
        OrientationList = orientationList;
    }

}

